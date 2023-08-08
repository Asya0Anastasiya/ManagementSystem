using ManagementSystem.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Data
{
    public class Context : IdentityDbContext<UserIdentity>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { 
        }

        //public DbSet<UserEntity> AppUsers { get; set; }
    }
}
