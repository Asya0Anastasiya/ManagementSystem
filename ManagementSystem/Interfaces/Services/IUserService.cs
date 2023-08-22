using System.Globalization;
using TimeTrackingService.Models.Entities;
using UserServiceAPI.Models.UserDto;

namespace UserServiceAPI.Interfaces.Services
{
    public interface IUserService
    {
        public Task Create(SignUpModel user);

        public string Login(SignInModel signInModel);

        public Task<List<UserInfoModel>> GetUsersAsync();

        public void ChangePassword(Guid id, string oldPassword, string newPassword);

        //public List<DaysAccounting> GetDays(Guid id);

        public Task<UserInfoModel> GetUserInfo(string email, int month);
    }
}
