using System.ComponentModel.DataAnnotations;
using TimeTrackingService.Models.Enums;

namespace TimeTrackingService.Models.Dto
{
    public class CreateDayModel
    {
        [Required]
        public int Hours { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int AccountingType { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
