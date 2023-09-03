using System.ComponentModel.DataAnnotations;

namespace UserServiceAPI.Models.UserDto
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
        [MaxLength(30)]
        public string Department { get; set; }

        [Required]
        [MaxLength(30)]
        public string Position { get; set; }

        [Required]
        [MaxLength(20)]
        public string Level { get; set; }

        [Required]
        [MaxLength(15)]
        public string Phone { get; set; }
    }
}
