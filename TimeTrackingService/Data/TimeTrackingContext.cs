using Microsoft.EntityFrameworkCore;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Data
{
    public class TimeTrackingContext : DbContext
    {
        public TimeTrackingContext(DbContextOptions<TimeTrackingContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        // https://signup.azure.com/screen
        public DbSet<DayAccounting> DaysAccounting { get; set;}
        public DbSet<Document> Documents { get; set;}
    }
}
