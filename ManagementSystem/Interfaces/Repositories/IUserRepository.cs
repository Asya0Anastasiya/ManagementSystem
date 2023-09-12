using UserServiceAPI.Helpers;
using UserServiceAPI.Helpers.Pagination;
using UserServiceAPI.Models.Entities;

namespace UserServiceAPI.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task CreateUserAsync(UserEntity userEntity);

        public Task<List<UserEntity>> GetUsersAsync(FilteringParameters parameters, PaginationParameters pagination);

        public int GetUsersCount();

        public Task<UserEntity> GetUserByEmailAsync(string email);

        public Task UpdateUserAsync(UserEntity userEntity);

        public Task DeleteUserAsync(UserEntity userEntity);

        public Task<UserEntity> GetUserByIdAsync(Guid id);
    }
}
