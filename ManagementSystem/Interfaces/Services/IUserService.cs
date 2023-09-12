using System.Globalization;
using TimeTrackingService.Models.Entities;
using UserServiceAPI.Helpers;
using UserServiceAPI.Helpers.Filtering;
using UserServiceAPI.Helpers.Pagination;
using UserServiceAPI.Models.UserDto;

namespace UserServiceAPI.Interfaces.Services
{
    public interface IUserService
    {
        public Task Create(SignUpModel user);

        public Task<string> Login(SignInModel signInModel);

        public Task<List<UserInfoModel>> GetUsersAsync(FilteringParameters parameters, PaginationParameters pagination);

        public int GetUsersCount();

        public Task ChangePassword(Guid id, string oldPassword, string newPassword);

        public Task<UserInfoModel> GetUserInfo(Guid id);

        public Task DeleteUserAsync(Guid id);

        public Task UpdateUserAsync(UserInfoModel user);

        public Task SetUserImageAsync(Guid userId, IFormFile file);

        public Task<byte[]> GetUserImageAsync(Guid userId);
    }
}
