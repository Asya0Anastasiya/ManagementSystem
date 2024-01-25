namespace UserService.Models.Entities
{
    public class DepartmentEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<PositionEntity> Positions { get; set; }

        public BranchOfficeEntity BranchOffice { get; set; } = null!;

        public Guid BranchOfficeId { get; set; }
    }
}
