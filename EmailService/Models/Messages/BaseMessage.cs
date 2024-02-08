using EmailService.Models.Enums;

namespace EmailService.Models.Messages
{
    public class BaseMessage
    {
        public MessageTypes MessageType { get; set; }
    }
}
