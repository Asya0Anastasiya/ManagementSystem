namespace UserService.Models.Entities
{
    public class BranchOfficeEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<DepartmentEntity> Departments { get; set; }

        public AddressEntity Address { get; set; }

        public Guid AddressId { get; set; }
    }
}
