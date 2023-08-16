using ManagementSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Models.Entities
{
    public class UserEntity
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public Roles Role { get; set; }

        public string Token { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
