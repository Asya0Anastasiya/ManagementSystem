using UserService.Helpers;
using UserService.Helpers.Pagination;
using UserService.Models.Entities;

namespace UserService.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task CreateUserAsync(UserEntity userEntity);

        public Task<List<UserEntity>> GetUsersAsync(FilteringParameters parameters, PaginationParameters pagination);

        public Task<int> GetUsersCountAsync();

        public Task<UserEntity> GetUserByEmailAsync(string email);

        public Task UpdateUserAsync(UserEntity userEntity);

        public Task DeleteUserAsync(UserEntity userEntity);

        public Task<UserEntity> GetUserByIdAsync(Guid id);

        public Task RemoveRefreshTokenAsync(UserEntity user);

        public Task AddRefreshTokenAsync(RefreshToken refreshToken);

        public Task<UserEntity> GetUserByRefreshTokenAsync(string refreshToken);

    }
}
