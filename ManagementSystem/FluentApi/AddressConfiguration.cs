using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Models.Entities;

namespace UserService.FluentApi
{
    public class AddressConfiguration : IEntityTypeConfiguration<AddressEntity>
    {
        public void Configure(EntityTypeBuilder<AddressEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(address => address.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .Property(address => address.Country)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .Property(address => address.City)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .Property(address => address.Street)
                .IsRequired()
                .HasMaxLength(35);

            builder
                .Property(address => address.HouseNumber)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
