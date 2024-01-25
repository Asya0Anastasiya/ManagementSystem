namespace UserService.Models.Entities
{
    public class PositionEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DepartmentEntity Department { get; set; } = null!;

        public Guid DepartmentId { get; set; }
    }
}
