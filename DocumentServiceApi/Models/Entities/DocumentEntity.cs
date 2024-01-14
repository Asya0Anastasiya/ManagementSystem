using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DocumentServiceApi.Models.Enums;

namespace DocumentServiceApi.Models.Entities
{
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
