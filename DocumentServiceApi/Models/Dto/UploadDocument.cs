namespace DocumentServiceApi.Models.Dto
{
    public class UploadDocument
    {
        public IFormFile File { get; set; }

        public string Type { get; set; }

        public Guid UserId { get; set; }
    }
}
