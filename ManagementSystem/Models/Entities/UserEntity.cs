using UserService.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models.Entities
{
    public class UserEntity
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public PositionEntity Position { get; set; }

        [Required]
        public Guid PositionId { get; set; }

        [Required]
        public Roles Role { get; set; } = Roles.User; 

        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        // !!!!!!!!!!!!!!!!
        [MaxLength(65)]
        public string Password { get; set; } = "";

        public byte[] UserImage { get; set; }
    }
}
