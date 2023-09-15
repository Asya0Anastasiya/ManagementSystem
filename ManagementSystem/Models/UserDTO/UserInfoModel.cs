using System.ComponentModel.DataAnnotations;

namespace UserServiceAPI.Models.UserDto
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
        [MaxLength(20)]
        public string Department { get; set; }

        [Required]
        [MaxLength(20)]
        public string Position { get; set; }

        [Required]
        [MaxLength(20)]
        public string Level { get; set; }
    }
}
