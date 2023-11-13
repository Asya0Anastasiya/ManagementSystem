using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Enums;

namespace TimeTrackingService.Models.Messages
{
    public class TimeTrackDocumentUploadedMessage : BaseMessage
    {
        public TimeTrackDocumentUploadedMessage()
        {
            MessageType = MessageTypes.TimeTrackDocumentUploaded;
        }

        public UpcomingDocumentModel DocumentModel { get; set; }
    }
}
