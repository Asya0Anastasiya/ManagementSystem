using UserServiceAPI.Models.Entities;

namespace UserServiceAPI.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task CreateUserAsync(UserEntity userEntity);

        public Task<List<UserEntity>> GetUsersAsync();

        //public Task<bool> CheckUserExistAsync(string email);

        public Task<UserEntity> GetUserByEmailAsync(string email);

        public Task UpdateUserAsync(UserEntity userEntity);

        public UserEntity GetUserById(Guid id);
    }
}
