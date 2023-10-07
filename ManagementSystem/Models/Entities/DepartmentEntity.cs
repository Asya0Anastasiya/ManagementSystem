using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserService.Models.Entities
{
    public class DepartmentEntity
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public ICollection<PositionEntity> Positions { get; set; }

        public BranchOfficeEntity BranchOffice { get; set; } = null!;

        [Required]
        public Guid BranchOfficeId { get; set; }
    }
}
