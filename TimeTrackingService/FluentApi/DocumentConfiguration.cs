using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.FluentApi
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
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
                .HasMaxLength(50);

            builder
                .Property(doc => doc.UserId)
                .IsRequired();

            builder
                .Property(doc => doc.SourceId)
                .IsRequired();

            builder
                .Property(doc => doc.Type)
                .IsRequired();

            builder
                .HasMany(doc => doc.DaysAccounting)
                .WithMany(doc => doc.Documents);
        }
    }
}
