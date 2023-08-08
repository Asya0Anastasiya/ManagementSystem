using Microsoft.AspNetCore.Identity;

namespace ManagementSystem.Entities
{
    public class UserIdentity : IdentityUser
    {
        public string Firstname { get; set; }

        public string Lasttname { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
