using Microsoft.EntityFrameworkCore;
using UserServiceAPI.Data;
using UserServiceAPI.Interfaces.Repositories;
using UserServiceAPI.Models.Entities;

namespace UserServiceAPI.Repositories
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
    }
}
