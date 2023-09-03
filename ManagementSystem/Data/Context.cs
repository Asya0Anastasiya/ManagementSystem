using UserServiceAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserServiceAPI.Data
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
