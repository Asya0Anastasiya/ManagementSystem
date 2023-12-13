using Microsoft.EntityFrameworkCore;
using UserService.FluentApi;

namespace UserService.Models.Entities
{
    [EntityTypeConfiguration(typeof(PositionConfiguration))]
    public class PositionEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DepartmentEntity Department { get; set; } = null!;

        public Guid DepartmentId { get; set; }
    }
}
