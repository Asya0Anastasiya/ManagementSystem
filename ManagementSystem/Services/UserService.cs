using ManagementSystem.Data;
using ManagementSystem.Entities;
using ManagementSystem.Interfaces;

namespace ManagementSystem.Services
{
    public class UserService : IUser
    {
        private readonly Context context;
        public UserService(Context _context)
        {
            context = _context;
        }

        public void Create(UserEntity entity)
        {
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
