using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TimeTrackingService.Models.Enums;
using Microsoft.EntityFrameworkCore;
using TimeTrackingService.FluentApi;

namespace TimeTrackingService.Models.Entities
{
    [EntityTypeConfiguration(typeof(DocumentConfiguration))]
    public class Document
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid UserId { get; set; }

        public Types Type { get; set; }

        public Guid SourceId { get; set; }

        public List<DayAccounting> DaysAccounting { get; set; } = new List<DayAccounting>();
    }
}
