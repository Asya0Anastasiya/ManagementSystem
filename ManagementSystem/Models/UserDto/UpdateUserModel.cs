using System.ComponentModel.DataAnnotations;

namespace UserService.Models.UserDto
{
    public class UpdateUserModel
    {
        // убрать required, если модель не приходит с фронта
        [Required]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string Email { get; set; }

        [Required, Phone, MaxLength(20)]
        public string PhoneNumber { get; set; }
    }
}
