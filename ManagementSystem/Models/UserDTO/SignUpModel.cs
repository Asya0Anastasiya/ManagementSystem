using System.ComponentModel.DataAnnotations;

namespace UserService.Models.UserDto
{
    public class SignUpModel
    {
        [Required, MaxLength(50)]
        public string Firstname { get; set; }

        [Required, MaxLength(50)]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength (25)]
        public string Password { get; set; }

        [Required]
        public Guid PositionId { get; set; }

        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
    }
}
