using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models.Entities
{
    public class AddressEntity
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Country { get; set; }

        [Required]
        [MaxLength(30)]
        public string City { get; set; }

        [Required]
        [MaxLength(35)]
        public string Street { get; set; }

        [Required]
        [MaxLength(10)]
        public string Building { get; set; }
    }
}
