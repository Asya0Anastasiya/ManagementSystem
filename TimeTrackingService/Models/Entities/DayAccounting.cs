using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TimeTrackingService.Models.Enums;

namespace TimeTrackingService.Models.Entities
{
    public class DayAccounting
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required, Range(0, 12)]
        public int Hours { get; set; } = 8;

        [Required, Range(1, 31)]
        public int Day { get; set; } = 1;

        [Required, Range(1, 12)]
        public int Month { get; set; } = 1;

        [Required]
        public int Year { get; set; } = 1;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public AccountingTypes? AccountingType { get; set; }

        [Required]
        public bool IsConfirmed { get; set; } = false;

        [Required]
        public Guid UserId { get; set; }

        public List<Document> Documents { get; set; } = new List<Document>();
    }
}
