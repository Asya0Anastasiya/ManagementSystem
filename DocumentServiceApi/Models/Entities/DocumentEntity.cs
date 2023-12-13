using DocumentServiceApi.Models.Enums;
using Microsoft.EntityFrameworkCore;
using DocumentServiceApi.FluentApi;

namespace DocumentServiceApi.Models.Entities
{
    [EntityTypeConfiguration(typeof(DocumentConfiguration))]
    public class DocumentEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Size { get; set; }

        public string ContentType { get; set; }

        public Types Type { get; set; }

        public DateTime UploadDate { get; set; }

        public Guid UserId { get; set; }
    }
}
