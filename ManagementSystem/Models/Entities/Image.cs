using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserServiceAPI.Models.Entities
{
    public class Image
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; } 

        [Required]
        public byte[] Data { get; set; }

        [Required]
        public Guid UserId { get; set; }

        //[Required]
        //public UserEntity User { get; set; }
    }
}
