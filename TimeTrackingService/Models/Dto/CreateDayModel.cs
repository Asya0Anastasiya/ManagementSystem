using TimeTrackingService.Models.Enums;

namespace TimeTrackingService.Models.Dto
{
    public class CreateDayModel
    {
        public int Hours { get; set; }

        public DateTime Date { get; set; }

        public AccountingTypes AccountingType { get; set; }

        public Guid UserId { get; set; }

        public bool IsConfirmed { get; set; }
    }
}
