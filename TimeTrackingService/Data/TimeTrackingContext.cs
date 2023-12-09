using Microsoft.EntityFrameworkCore;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Data
{
    public class TimeTrackingContext : DbContext
    {
        public TimeTrackingContext(DbContextOptions<TimeTrackingContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<DayAccounting> DaysAccounting { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}
