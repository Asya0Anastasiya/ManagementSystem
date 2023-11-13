using DocumentServiceApi.Models.Enums;

namespace DocumentServiceApi.Models.Messages
{
    public class BaseMessage
    {
        public MessageTypes MessageType { get; set; }
    }
}
