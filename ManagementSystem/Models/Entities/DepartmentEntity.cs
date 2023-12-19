using UserService.FluentApi;
using Microsoft.EntityFrameworkCore;

namespace UserService.Models.Entities
{
    [EntityTypeConfiguration(typeof(DepartmentConfiguration))]
    public class DepartmentEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<PositionEntity> Positions { get; set; }

        public BranchOfficeEntity BranchOffice { get; set; } = null!;

        public Guid BranchOfficeId { get; set; }
    }
}
