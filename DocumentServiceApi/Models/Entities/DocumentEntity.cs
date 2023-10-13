using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DocumentServiceApi.Models.Entities
{
    public class DocumentEntity
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        [Range(0, 10000)]
        public double Size { get; set; }

        [Required]
        [MaxLength(20)]
        public string ContentType { get; set; }

        [Required]
        [MaxLength(20)]
        public string Type { get; set; }

        [Required]
        [MaxLength(60)]
        public DateTime UploadDate { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
