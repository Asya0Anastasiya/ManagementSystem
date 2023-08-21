using System.ComponentModel.DataAnnotations;
using TimeTrackingService.Models.Enums;

namespace TimeTrackingService.Models.Dto
{
    public class DaysAccountingModel
    {
        [Required, Range(0, 12)]
        public int Hours { get; set; } = 8;

        [Required, Range(1, 31)]
        public int Day { get; set; }

        [Required, Range(1, 12)]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public AccountingTypes AccountingType { get; set; }
    }
}
