namespace TimeTrackingService.Models.Dto
{
    public class UsersDaysModel
    {
        public Guid UserId { get; set; }

        public int WorkDaysCount { get; set; }

        public int SickDaysCount { get; set; }

        public int HolidaysCount { get; set; }

        public int PaidDaysCount { get; set; }
    }
}
