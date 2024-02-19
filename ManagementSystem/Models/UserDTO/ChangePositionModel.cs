namespace UserService.Models.UserDTO
{
    public class ChangePositionModel
    {
        public Guid UserId { get; set; }

        public Guid AdminId { get; set; }

        public Guid PositionId { get; set; }
    }
}
