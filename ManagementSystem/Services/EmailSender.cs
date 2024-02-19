using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using UserService.Interfaces.Services;
using UserService.Models.MessageDto;
using UserService.Models.Options;

namespace UserService.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions _emailOptions;

        public EmailSender(IOptions<EmailOptions> emailConfig)
        {
            _emailOptions = emailConfig.Value;
        }

        public async Task SendEmail(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);

            await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("email", _emailOptions.From));

            emailMessage.To.AddRange(message.To);

            emailMessage.Subject = message.Subject;

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailOptions.SmtpServer, _emailOptions.Port, true);

                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    await client.AuthenticateAsync(_emailOptions.UserName, _emailOptions.Password);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);

                    client.Dispose();
                }
            }
        }
    }
}
