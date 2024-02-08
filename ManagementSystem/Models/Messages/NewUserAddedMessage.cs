using UserService.Models.Enums;
using UserService.Models.UserDTO;

namespace UserService.Models.Messages
{
    public class NewUserAddedMessage : BaseMessage
    {
        public NewUserAddedMessage()
        {
            MessageType = MessageTypes.NewUserAdded;
        }

        public UserCredentials UserCredentials { get; set; }
    }
}
