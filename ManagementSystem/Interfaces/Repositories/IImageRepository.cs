using UserServiceAPI.Data;
using UserServiceAPI.Models.Entities;

namespace UserServiceAPI.Interfaces.Repositories
{
    public interface IImageRepository 
    {
        public Task SetUserImageAsync(Image image);

        public Task<Image> GetUserImageAsync(Guid userId);

        public Task RemoveUserImageAsync(Guid userId);
    }
}
