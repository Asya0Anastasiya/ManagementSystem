using ManagementSystem.Entities;
using Microsoft.AspNetCore.Identity;

namespace ManagementSystem.Interfaces
{
    public interface IUser
    {
        //public UserEntity GetById(int id);

        public List<UserIdentity> GetAll();

        public Task<IdentityResult> CreateIdentity(SignUpEntity userLogin);

        public Task<SignInResult> PasswordSignInAsync(SignInEntity signInEntity);

        public Task SignOutAsync();

        public string GetUserId();

        public bool IsAuthenticated();

        public Task<IdentityResult> ChangePasswordAsync(ChangePassword changePassword);

    }
}
