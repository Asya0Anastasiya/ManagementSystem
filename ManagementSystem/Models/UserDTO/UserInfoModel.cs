using System.ComponentModel.DataAnnotations;

namespace UserService.Models.UserDto
{
    public class UserInfoModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string PhoneNumber { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }

        public string BranchOffice { get; set; }
    }
}
