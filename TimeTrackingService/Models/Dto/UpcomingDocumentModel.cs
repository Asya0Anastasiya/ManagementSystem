
namespace TimeTrackingService.Models.Dto
{
    public class UpcomingDocumentModel
    {
        public string Name { get; set; }

        public Guid UserId { get; set; }

        public string Type { get; set; }

        public Guid SourceId { get; set; }

        public DateTime Date { get; set; }
    }
}
