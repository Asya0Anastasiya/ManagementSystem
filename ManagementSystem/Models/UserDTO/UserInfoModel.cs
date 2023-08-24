namespace UserServiceAPI.Models.UserDto
{
    public class UserInfoModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int WorkDays { get; set; }

        public int SickDays { get; set; }

        public int Holidays { get; set; }

        public int PaidDays { get; set; }
    }
}
