namespace DocumentServiceApi.Models.Dto
{
    public class DocumentDto
    {
        public string Name { get; set; }

        public string ContentType { get; set; }

        public MemoryStream Stream { get; set; }
    }
}
