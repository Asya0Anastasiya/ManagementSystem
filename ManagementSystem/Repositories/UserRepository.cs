﻿using UserServiceAPI.Data;
using UserServiceAPI.Interfaces.Repositories;
using UserServiceAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserServiceAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context context;

        public UserRepository(Context _context) 
        {
            context = _context;
        }

        public async Task CreateUserAsync(UserEntity userEntity)
        {
            context.Users.Add(userEntity);
            await context.SaveChangesAsync();
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email.ToUpper() == email.Trim().ToUpper());
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

        public async Task DeleteUserAsync(UserEntity user)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task<UserEntity> GetUserByIdAsync(Guid id)
        {
            // не работает асинхронный метод  

            //can't parse JSON.  Raw result:

            //Serialization and deserialization of 'System.Action' instances are not supported. Path: $.MoveNextAction.
            return await context.Users.FindAsync(id);
        }
    }
}
