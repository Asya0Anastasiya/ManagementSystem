using UserService.Models.MessageDto;

namespace UserService.Interfaces.Services
{
    public interface IEmailSender
    {
        public Task SendEmail(EmailMessage message);
    }
}
