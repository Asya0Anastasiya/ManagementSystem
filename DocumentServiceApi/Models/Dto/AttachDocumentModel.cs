using DocumentServiceApi.Models.Enums;

namespace DocumentServiceApi.Models.Dto
{
    public class AttachDocumentModel
    {
        public string Name { get; set; }

        public Types Type { get; set; }

        public Guid UserId { get; set; }

        public Guid SourceId { get; set; }

        public DateTime? Date { get; set; }
    }
}
