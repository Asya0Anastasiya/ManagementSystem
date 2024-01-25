namespace UserService.Models.UserDto
{
    public class SignUpModel
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Guid PositionId { get; set; }

        public string PhoneNumber { get; set; }
    }
}
