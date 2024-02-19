using UserService.Helpers.Pagination;
using UserService.Models.Entities;
using UserService.Models.TokenDto;
using UserService.Models.Params;
using UserService.Models.UserDto;
using UserService.Models.UserDTO;

namespace UserService.Interfaces.Services
{
    public interface IUserService
    {
        public Task Create(SignUpModel user);

        public Task<Tokens> Login(SignInModel signInModel);

        public Task<List<UserInfoModel>> GetUsersAsync(FilteringParameters parameters, int pageNumber, int pageSize);

        public Task<int> GetUsersCountAsync();

        public Task ChangePassword(Guid id, string oldPassword, string newPassword);

        public Task<UserInfoModel> GetUserInfo(Guid id);

        public Task DeleteUserAsync(Guid id);

        public Task UpdateUserAsync(UpdateUserModel user);

        public Task SetUserImageAsync(Guid userId, IFormFile file);

        public Task<byte[]> GetUserImageAsync(Guid userId);

        public Task<Tokens> ValidateRefreshTokenAsync(string refreshToken);

        public Task ChangeUserPermissionsAsync(ChangePermissionsModel permissionsModel);

        public Task ChangeUserPositionAsync(ChangePositionModel positionModel);
    }
}
