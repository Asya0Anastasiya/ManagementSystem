using UserService.Data;
using UserService.Models.Entities;

namespace UserService.Interfaces.Repositories
{
    public interface IImageRepository 
    {
        public Task SetUserImageAsync(Image image);

        public Task<Image> GetUserImageAsync(Guid userId);

        public Task RemoveUserImageAsync(Guid userId);
    }
}
