using TimeTrackingService.Models.Enums;

namespace TimeTrackingService.Helpers.Filtering
{
    public class FilteringParameters
    {
        public Guid UserId { get; set; }

        public int? Month { get; set; }

        public int? FromDay { get; set; }

        public int? TillDay { get; set; }

        public AccountingTypes? AccountingType { get; set; }
    }
}
