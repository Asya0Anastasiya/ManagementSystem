using UserService.Data;
using UserService.Interfaces.Repositories;
using UserService.Models.Entities;
using Microsoft.EntityFrameworkCore;
using UserService.Helpers.Pagination;
using UserService.Helpers.Filtering;
using UserService.Helpers;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context) 
        {
            _context = context;
        }

        public async Task CreateUserAsync(UserEntity userEntity)
        {
            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToUpper() == email.Trim().ToUpper());
        }

        public async Task<List<UserEntity>> GetUsersAsync(FilteringParameters parameters,
                                                          PaginationParameters pagination)
        {
            var users = _context.Users
                .Include(user => user.Position)
                .ThenInclude(position => position.Department)
                .ThenInclude(department => department.BranchOffice).AsQueryable();
            FilteringHelper filteringHelper = new();
            users = filteringHelper.FilterUsers(parameters, users);
            return PagedList<UserEntity>.ToPagedItems(users, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(UserEntity user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserEntity> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users
                .Include(user => user.Position)
                .ThenInclude(position => position.Department)
                .ThenInclude(department => department.BranchOffice)
                .AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        //public async Task SetUserImageAsync(Image image)
        //{
        //    await _context.Images.AddAsync(image);
        //    await _context.SaveChangesAsync();
        //}

        public async Task<byte[]> GetUserImageAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return user.UserImage;
        }

        //public async Task RemoveUserImageAsync(Guid userId)
        //{
        //    var image = await GetUserImageAsync(userId);

        //    if (image != null)
        //    {
        //        _context.Images.Remove(await GetUserImageAsync(userId));
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
