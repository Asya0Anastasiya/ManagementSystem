using UserService.Models.Entities;
using UserService.Models.UserDto;

namespace Tests.MockData
{
    public static class UserMockData
    {
        public static List<UserInfoModel> GetUsersInfoModel()
        {
            return new List<UserInfoModel>() 
            {
                new UserInfoModel()
                {
                    Id = new Guid("c5842e31-2f98-409b-2cd6-08dbbf946b0b"),
                    FirstName = "Rafael",
                    LastName = "Santi",
                    Email = "Santii@gmail.com",
                    PhoneNumber = "1234567890",
                    Position = ".Net Engineer",
                    Department = "Software Dev",
                    BranchOffice = "Epam"
                }
            };
        }

        public static List<UserEntity> GetUsers()
        {
            return new List<UserEntity>()
            {
                new UserEntity()
                {
                    Id = new Guid("ad229ca8-8e44-4124-9b05-124c020adda8"),
                    FirstName = "Rafael",
                    LastName = "Santi",
                    Email = "Santii@gmail.com",
                    PhoneNumber = "1234567890",
                    PositionId = new Guid("c5842e31-2f98-409b-2cd6-08dbbf946b0b"),
                }
            };
        }
    }
}
