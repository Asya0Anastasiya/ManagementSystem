using ManagementSystem.Entities;

namespace ManagementSystem.Interfaces
{
    public interface IUser
    {
        public void Create(UserEntity entity);

        public UserEntity GetById(int id);

        public List<UserEntity> GetAll();
    }
}
