using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Models.UserModels
{
    public class SignUpModel
    {
        [Required, MinLength(2)]
        public string Firstname { get; set; }

        [Required, MinLength(2)]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        [MinLength(10)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
