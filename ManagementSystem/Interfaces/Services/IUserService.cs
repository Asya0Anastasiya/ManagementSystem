using ManagementSystem.Models;
using ManagementSystem.Models.UserDTO;
using ManagementSystem.Models.UserModels;

namespace ManagementSystem.Interfaces.Services
{
    public interface IUserService
    {
        public Task Create(SignUpModel user);

        public string Login(SignInModel signInModel);

        public Task<List<UserInfoModel>> GetUsers();

        public void ChangePassword(Guid id, string oldPassword, string newPassword);
    }
}
