using TimeTrackingService.Models.Enums;

namespace TimeTrackingService.Models.Entities
{
    public class Document
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid UserId { get; set; }

        public DocumentTypes Type { get; set; }

        public Guid SourceId { get; set; }

        public List<DayAccounting> DaysAccounting { get; set; } = new List<DayAccounting>();
    }
}
