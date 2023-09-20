using System.ComponentModel.DataAnnotations;

namespace UserService.Models.UserDto
{
    public class ChangePasswordModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
