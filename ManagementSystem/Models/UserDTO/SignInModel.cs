using System.ComponentModel.DataAnnotations;

namespace UserService.Models.UserDto
{
    public class SignInModel
    {
        [Required, EmailAddress, MaxLength(50)]
        public string Email { get; set; }

        [Required, MaxLength(25)]
        public string Password { get; set; }
    }
}
