using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Models.Entities;
using UserService.Models.Enums;

namespace UserService.FluentApi
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder
                .HasKey(user => user.Id);

            builder
                .Property(user => user.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .Property(user => user.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(user => user.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(user => user.PhoneNumber)
                .HasMaxLength(15);

            builder
                .Property(user => user.Password)
                .IsRequired()
                .HasMaxLength(65);

            builder
                .Property(user => user.UserImage);

            builder
                .HasOne(user => user.Position)
                .WithMany()
                .HasForeignKey(user => user.PositionId);

            builder
                .Property(user => user.Role)
                .HasDefaultValue(Roles.User);
        }
    }
}
