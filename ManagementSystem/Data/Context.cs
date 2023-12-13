using UserService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserService.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PositionEntity> Positions { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<BranchOfficeEntity> Branches { get; set; }
        public DbSet<BranchOfficeEntity> Addresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BranchOfficeEntity>().HasData(new BranchOfficeEntity()
            {
                Id = new Guid("74c7e715-7af4-41e4-a081-1588550cb9ef"),
                Country = "Belarus",
                City = "Hrodna",
                Street = "Repina",
                HouseNumber = "3"
            });

            modelBuilder.Entity<BranchOfficeEntity>().HasData(new BranchOfficeEntity()
            {
                Id = new Guid("983662f2-fd59-4ce6-8241-dc7cb879d2dc"),
                Name = "iTech-Art.Hrodno",
                AddressId = new Guid("74c7e715-7af4-41e4-a081-1588550cb9ef")
            });

            modelBuilder.Entity<DepartmentEntity>().HasData(new DepartmentEntity()
            {
                Id = new Guid("96b89eee-cc6d-41a9-bfe2-13d8cc7afb62"),
                Name = "Back-end Development",
                BranchOfficeId = new Guid("983662f2-fd59-4ce6-8241-dc7cb879d2dc")
            });

            modelBuilder.Entity<PositionEntity>().HasData(new PositionEntity()
            {
                Id = new Guid("6ac1ec21-6231-4c69-a508-15c1e99ff235"),
                Name = ".Net Developer",
                DepartmentId = new Guid("96b89eee-cc6d-41a9-bfe2-13d8cc7afb62")
            });

            modelBuilder.Entity<UserEntity>().HasData(new UserEntity()
            {
                Id = new Guid("c5842e31-2f98-409b-2cd6-08dbbf946b0b"),
                FirstName = "Eva",
                LastName = "Cassidy",
                Email = "Eva@gmail.com",
                PositionId = new Guid("6ac1ec21-6231-4c69-a508-15c1e99ff235"),
                Role = Models.Enums.Roles.Admin,
                PhoneNumber = "+375295647467",
                Password = "$2a$11$.hZl.BNBmGHlNvLcADCKyeswAYtHB3pE1kbM22ksSI3Q8eBYOrAh."
            });
        }
    }
}
