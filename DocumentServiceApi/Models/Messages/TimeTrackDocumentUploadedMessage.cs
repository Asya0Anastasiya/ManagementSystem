using DocumentServiceApi.Models.Dto;
using DocumentServiceApi.Models.Enums;

namespace DocumentServiceApi.Models.Messages
{
    public class TimeTrackDocumentUploadedMessage : BaseMessage
    {
        public TimeTrackDocumentUploadedMessage()
        {
            MessageType = MessageTypes.TimeTrackDocumentUploaded;
        }

        public AttachDocumentModel DocumentModel { get; set; }
    }
}
