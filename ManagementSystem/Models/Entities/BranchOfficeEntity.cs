using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserService.Models.Entities
{
    public class BranchOfficeEntity
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public ICollection<DepartmentEntity> Departments { get; set; }

        [Required]
        public AdressEntity Adress { get; set; }

        [Required]
        public Guid AdressId { get; set; }
    }
}
