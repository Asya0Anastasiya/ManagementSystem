namespace DocumentServiceApi.Models.Dto
{
    public class AttachDocumentModel
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public Guid UserId { get; set; }

        public Guid SourceId { get; set; }

        public DateTime Date { get; set; }
    }
}
