using ManagementSystem.Data;
using ManagementSystem.Entities;
using ManagementSystem.Interfaces;

namespace ManagementSystem.Services
{
    public class UserService : IUser
    {
        private readonly Context context;
        private readonly IPassword iPassword;

        public UserService(Context _context, IPassword _password)
        {
            context = _context;
            iPassword = _password;
        }

        public void Create(UserEntity entity)
        {
            iPassword.CreatePasswordHash(entity.Password, out byte[] passwordHash, out byte[] passwordSalt);
            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;
            context.Users.Add(entity);
            context.SaveChanges();
        }

        public List<UserEntity> GetAll()
        {
            return context.Users.ToList();
        }

        public UserEntity GetById(int id)
        {
            var user = context.Users.Find(id);

            if (user != null) return user;
            return null;
        }
    }
}
