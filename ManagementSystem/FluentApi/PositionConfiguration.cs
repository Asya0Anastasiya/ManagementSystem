using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Models.Entities;

namespace UserService.FluentApi
{
    public class PositionConfiguration : IEntityTypeConfiguration<PositionEntity>
    {
        public void Configure(EntityTypeBuilder<PositionEntity> builder)
        {
            builder
                .HasKey(position => position.Id);

            builder
                .Property(position => position.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .Property(position => position.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .HasOne(position => position.Department)
                .WithMany(department => department.Positions)
                .HasForeignKey(position => position.DepartmentId);
        }
    }
}
