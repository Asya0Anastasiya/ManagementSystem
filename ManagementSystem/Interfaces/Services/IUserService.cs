using System.Globalization;
using TimeTrackingService.Models.Entities;
using UserService.Helpers;
using UserService.Helpers.Filtering;
using UserService.Helpers.Pagination;
using UserService.Models.UserDto;

namespace UserService.Interfaces.Services
{
    public interface IUserService
    {
        public Task Create(SignUpModel user);

        public Task<string> Login(SignInModel signInModel);

        public Task<List<UserInfoModel>> GetUsersAsync(FilteringParameters parameters, PaginationParameters pagination);

        public Task<int> GetUsersCountAsync();

        public Task ChangePassword(Guid id, string oldPassword, string newPassword);

        public Task<UserInfoModel> GetUserInfo(Guid id);

        public Task DeleteUserAsync(Guid id);

        public Task UpdateUserAsync(UpdateUserModel user);

        public Task SetUserImageAsync(Guid userId, IFormFile file);

        public Task<byte[]> GetUserImageAsync(Guid userId);
    }
}
