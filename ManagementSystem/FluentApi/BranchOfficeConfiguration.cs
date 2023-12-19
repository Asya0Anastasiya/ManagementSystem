using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Models.Entities;

namespace UserService.FluentApi
{
    public class BranchOfficeConfiguration : IEntityTypeConfiguration<BranchOfficeEntity>
    {
        public void Configure(EntityTypeBuilder<BranchOfficeEntity> builder)
        {
            builder
                .HasKey(branchOffice => branchOffice.Id);

            builder
                .Property(branchOffice => branchOffice.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .Property(branchOffice => branchOffice.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .HasOne(branchOffice => branchOffice.Address)
                .WithMany()
                .HasForeignKey(branchOffice => branchOffice.AddressId);

            builder
                .HasMany(branchOffice => branchOffice.Departments)
                .WithOne(department => department.BranchOffice)
                .HasForeignKey(department => department.BranchOfficeId);
        }
    }
}
