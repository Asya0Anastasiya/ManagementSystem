using UserService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserService.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { 
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
