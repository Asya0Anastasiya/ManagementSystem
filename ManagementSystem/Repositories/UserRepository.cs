using UserServiceAPI.Data;
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

        public UserEntity GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email.ToUpper() == email.Trim().ToUpper());
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

        public UserEntity GetUserById(Guid id)
        {
            // не работает асинхронный метод  

            //can't parse JSON.  Raw result:

            //Serialization and deserialization of 'System.Action' instances are not supported. Path: $.MoveNextAction.
            return context.Users.Find(id);
        }
    }
}
