using ManagementSystem.Data;
using ManagementSystem.Exceptions;
using ManagementSystem.Helpers;
using ManagementSystem.Interfaces.Repositories;
using ManagementSystem.Models.Entities;
using ManagementSystem.Models.UserDTO;
using ManagementSystem.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context context;

        public UserRepository(Context _context) 
        {
            context = _context;
        }

        //public async Task<bool> CheckUserExistAsync(string email)
        //{
        //    return await context.Users.AnyAsync(u => u.Email.ToUpper() == email.Trim().ToUpper());
        //}

        public async Task CreateUserAsync(UserEntity userEntity)
        {
            context.Users.Add(userEntity);
            await context.SaveChangesAsync();
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email.ToUpper() == email.Trim().ToUpper());
        }

        public async Task<UserEntity> GetUserByIdAsync(Guid id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<UserEntity>> GetUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }
    }
}
