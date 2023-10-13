﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TimeTrackingService.Models.Entities
{
    public class Document
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; }

        [Required]
        public Guid SourceId { get; set; }

        public List<DayAccounting> DaysAccounting { get; set; }
    }
}