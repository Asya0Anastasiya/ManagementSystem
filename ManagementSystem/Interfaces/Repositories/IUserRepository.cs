using ManagementSystem.Models.Entities;
using ManagementSystem.Models.UserDTO;
using ManagementSystem.Models.UserModels;

namespace ManagementSystem.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task CreateUserAsync(SignUpModel signUpModel);

        public Task<List<UserInfoModel>> GetUsersAsync();

        public Task<bool> CheckUserExistAsync(string email);

        public Task<UserEntity> GetUserByIdAsync(Guid id);

        public Task<UserEntity> GetUserByEmailAsync(string email);

        public Task UpdateUserAsync(UserEntity userEntity);
    }
}
