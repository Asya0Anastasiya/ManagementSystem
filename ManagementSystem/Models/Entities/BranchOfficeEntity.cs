using Microsoft.EntityFrameworkCore;
using UserService.FluentApi;

namespace UserService.Models.Entities
{
    [EntityTypeConfiguration(typeof(BranchOfficeConfiguration))]
    public class BranchOfficeEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<DepartmentEntity> Departments { get; set; }

        public BranchOfficeEntity Address { get; set; }

        public Guid AddressId { get; set; }
    }
}
