using DocumentServiceApi.Models.Enums;

namespace DocumentServiceApi.Models.Dto
{
    public class UploadDocument
    {
        public IFormFile File { get; set; }

        public Types Type { get; set; }

        public Guid UserId { get; set; }
    }
}
