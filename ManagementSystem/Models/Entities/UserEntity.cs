using UserServiceAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserServiceAPI.Models.Entities
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
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public Roles Role { get; set; } = Roles.User; 

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string Password { get; set; } = "";
    }
}
