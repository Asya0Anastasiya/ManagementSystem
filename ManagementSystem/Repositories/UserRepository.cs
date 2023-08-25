using UserServiceAPI.Data;
using UserServiceAPI.Interfaces.Repositories;
using UserServiceAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using UserServiceAPI.Helpers;
using UserServiceAPI.Helpers.Pagination;

namespace UserServiceAPI.Repositories
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
            List<UserEntity> users = new();
            if (parameters.FirstName != null)
            {
                users.AddRange(await _context.Users.Where(x => x.FirstName.Contains(parameters.FirstName)).ToListAsync());
            }
            if (parameters.LastName != null)
            {
                users.AddRange(await _context.Users.Where(x => x.LastName.Contains(parameters.LastName)).ToListAsync());
            }
            if (parameters.Email != null)
            {
                users.AddRange(await _context.Users.Where(x => x.Email.Contains(parameters.Email)).ToListAsync());
            }
            if (parameters.DateOfBirth != null)
            {
                users.AddRange(await _context.Users.Where(x => x.DateOfBirth == parameters.DateOfBirth).ToListAsync());
            }
            if (parameters.PhoneNumber != null)
            {
                users.AddRange(await _context.Users.Where(x => x.FirstName.StartsWith(parameters.FirstName)).ToListAsync());
            }

            return await _context.Users
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();
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
            return await _context.Users.FindAsync(id);
        }
    }
}
