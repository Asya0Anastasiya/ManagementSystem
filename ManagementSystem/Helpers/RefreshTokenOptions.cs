namespace UserService.Helpers
{
    public class RefreshTokenOptions
    {
        public int RefreshTokenExpiresDays { get; set; }

        public int RefreshTokenExpiresHours { get; set; }

        public int RefreshTokenExpiresMinutes { get; set; }

        public int RefreshTokenExpiresSeconds { get; set; }
    }
}
