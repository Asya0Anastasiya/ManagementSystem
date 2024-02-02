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
        public DbSet<PositionEntity> Positions { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<BranchOfficeEntity> Branches { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
