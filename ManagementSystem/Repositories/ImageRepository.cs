using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Interfaces.Repositories;
using UserService.Models.Entities;

namespace UserService.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly Context _context;

        public ImageRepository(Context context)
        {
            _context = context;
        }

        public async Task SetUserImageAsync(Image image)
        {
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task<Image> GetUserImageAsync(Guid userId)
        {
            return await _context.Images.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task RemoveUserImageAsync(Guid userId)
        {
            var image = await GetUserImageAsync(userId);

            if (image != null)
            {
                _context.Images.Remove(await GetUserImageAsync(userId));
                await _context.SaveChangesAsync();
            }
        }      
    }
}
