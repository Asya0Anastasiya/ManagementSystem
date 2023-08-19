using Microsoft.EntityFrameworkCore;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Data
{
    public class TimeTrackingContext : DbContext
    {
        public TimeTrackingContext(DbContextOptions<TimeTrackingContext> options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<DaysAccounting>()
        //        .HasNoKey();
        //}

        public DbSet<DaysAccounting> DaysAccounting { get; set;}
    }
}
