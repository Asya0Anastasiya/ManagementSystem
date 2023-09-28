using System.ComponentModel.DataAnnotations;

namespace UserService.Models.UserDto
{
    public class UserInfoModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(30)]
        public string Department { get; set; }

        [Required]
        [MaxLength(30)]
        public string Position { get; set; }

        [Required]
        [MaxLength(30)]
        public string BranchOffice { get; set; }
    }
}
