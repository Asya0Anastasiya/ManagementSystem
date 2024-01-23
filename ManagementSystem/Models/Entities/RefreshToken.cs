namespace UserService.Models.Entities { 
public class RefreshToken
    {
        public Guid Id { get; set; }

        public string Token { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public Guid UserId { get; set; }

        public UserEntity User { get; set; }
    }
}
