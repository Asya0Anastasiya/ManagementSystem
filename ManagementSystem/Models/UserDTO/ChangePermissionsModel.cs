namespace UserService.Models.UserDTO
{
    public class ChangePermissionsModel
    {
        public Guid UserId { get; set; }

        public Guid AdminId { get; set; }

        public int NewRole { get; set; }
    }
}
