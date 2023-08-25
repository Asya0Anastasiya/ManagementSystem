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

        public DateTime? DateOfBirth { get; set; }

        [Required]
        public int WorkDays { get; set; }

        [Required]
        public int SickDays { get; set; }

        [Required]
        public int Holidays { get; set; }

        [Required]
        public int PaidDays { get; set; }
    }
}
