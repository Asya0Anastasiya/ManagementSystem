namespace DocumentServiceApi.Models.Dto
{
    public class DocumentInfo
    {
        public string Name { get; set; }

        public string ContentType { get; set; }

        public string Size { get; set; }

        public DateTime UploadDate { get; set; }
    }
}
