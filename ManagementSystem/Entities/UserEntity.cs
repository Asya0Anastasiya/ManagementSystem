using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, ErrorMessage = "Must be between 8 and 20 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
