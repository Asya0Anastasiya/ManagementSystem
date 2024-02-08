using EmailService.Models;

namespace EmailService.Interfaces
{
    public interface IEmailSender
    {
        public Task SendEmail(Message message);
    }
}
