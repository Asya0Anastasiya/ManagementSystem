using TimeTrackingService.Models.Enums;
using Microsoft.EntityFrameworkCore;
using TimeTrackingService.FluentApi;

namespace TimeTrackingService.Models.Entities
{
    [EntityTypeConfiguration(typeof(DayAccountingConfiguration))]
    public class DayAccounting
    {
        public Guid Id { get; set; }

        public int Hours { get; set; } = 8;

        public int Day { get; set; } = 1;

        public int Month { get; set; } = 1;

        public int Year { get; set; } = 1;

        public DateTime Date { get; set; }

        public AccountingTypes? AccountingType { get; set; }

        public bool IsConfirmed { get; set; } = false;

        public Guid UserId { get; set; }

        public List<Document> Documents { get; set; } = new List<Document>();
    }
}
