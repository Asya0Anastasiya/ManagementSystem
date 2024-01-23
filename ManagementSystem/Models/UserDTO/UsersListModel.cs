using UserService.Models.UserDto;

namespace UserService.Models.UserDTO
{
    public class UsersListModel
    {
        public List<UserInfoModel> UserInfoModels { get; set; }

        public int UsersCount { get; set; }
    }
}
