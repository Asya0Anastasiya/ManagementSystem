using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.FluentApi
{
    public class DayAccountingConfiguration : IEntityTypeConfiguration<DayAccounting>
    {
        public void Configure(EntityTypeBuilder<DayAccounting> builder)
        {
            builder
                .HasKey(day => day.Id);

            builder
                .Property(day => day.Id)
                .IsRequired();

            builder
                .Property(day => day.Hours)
                .IsRequired()
                .HasDefaultValue(8)
                .HasAnnotation("Range", new RangeAttribute(0, 12));

            builder
                .Property(day => day.Day)
                .IsRequired()
                .HasDefaultValue(1)
                .HasAnnotation("Range", new RangeAttribute(1, 31));

            builder
                .Property(day => day.Month)
                .IsRequired()
                .HasDefaultValue(1)
                .HasAnnotation("Range", new RangeAttribute(1, 12));

            builder
                .Property(day => day.Year)
                .IsRequired();

            builder
                .Property(day => day.Date)
                .IsRequired();

            builder
                .Property(day => day.AccountingType)
                .IsRequired();

            builder
                .Property(day => day.IsConfirmed)
                .IsRequired()
                .HasDefaultValue(false);

            builder
                .Property(day => day.UserId)
                .IsRequired();

            builder
                .HasMany(d => d.Documents)
                .WithMany(document => document.DaysAccounting);
        }
    }
}
