using ManagementSystem.Models;
using ManagementSystem.Models.UserModels;

namespace ManagementSystem.Interfaces
{
    public interface IUserService
    {
        public void Create(SignUpModel user);

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

        public string Login(SignInModel signInModel);

        public List<UserModel> GetUsers();

        public bool ChangePassword(string token, string oldPassword, string newPassword);
    }
}
