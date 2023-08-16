using ManagementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { 
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
