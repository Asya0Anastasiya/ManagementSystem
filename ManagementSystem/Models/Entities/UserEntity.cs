using UserService.Models.Enums;
using Microsoft.EntityFrameworkCore;
using UserService.FluentApi;

namespace UserService.Models.Entities
{
    [EntityTypeConfiguration(typeof(UserConfiguration))]
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public PositionEntity Position { get; set; }

        public Guid PositionId { get; set; }

        public Roles Role { get; set; } = Roles.User; 

        public string PhoneNumber { get; set; }

        public string Password { get; set; } = "";

        public byte[] UserImage { get; set; }
    }
}
