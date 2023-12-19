using DocumentServiceApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentServiceApi.FluentApi
{
    public class DocumentConfiguration : IEntityTypeConfiguration<DocumentEntity>
    {
        public void Configure(EntityTypeBuilder<DocumentEntity> builder)
        {
            builder
                .HasKey(doc => doc.Id);

            builder
                .Property(doc => doc.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .Property(doc => doc.Name)
                .IsRequired()
                .HasMaxLength(60);

            builder
                .Property(doc => doc.Size)
                .IsRequired()
                .HasAnnotation("Range", new RangeAttribute(0, 100000));

            builder
                .Property(doc => doc.ContentType)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(doc => doc.Type)
                .IsRequired();

            builder
                .Property(doc => doc.UserId)
                .IsRequired();
        }
    }
}
