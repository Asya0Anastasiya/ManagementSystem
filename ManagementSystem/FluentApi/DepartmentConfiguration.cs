using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Models.Entities;

namespace UserService.FluentApi
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<DepartmentEntity>
    {
        public void Configure(EntityTypeBuilder<DepartmentEntity> builder)
        {
            builder
                .HasKey(department => department.Id);

            builder
                .Property(department => department.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .Property(department => department.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .HasOne(department => department.BranchOffice)
                .WithMany(branchOffice => branchOffice.Departments)
                .HasForeignKey(department => department.BranchOfficeId);

            builder
                .HasMany(department => department.Positions)
                .WithOne(position => position.Department)
                .HasForeignKey(position => position.DepartmentId);
        }
    }
}
