using TimeTrackingService.Models.Enums;

namespace TimeTrackingService.Helpers.Filtering
{
    public class FilteringParameters
    {
        public Guid UserId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? TillDate { get; set; }

        public AccountingTypes? AccountingType { get; set; }
    }
}
