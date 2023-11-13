using TimeTrackingService.Models.Enums;

namespace TimeTrackingService.Models.Messages
{
    public class BaseMessage
    {
        public MessageTypes MessageType { get; set; }
    }
}
