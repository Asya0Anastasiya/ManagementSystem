using TimeTrackingService.Models.Enums;

namespace TimeTrackingService.Models.Params
{
    public class FilteringParameters
    {
        public Guid UserId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? TillDate { get; set; }

        public AccountingTypes? AccountingType { get; set; }
    }
}
