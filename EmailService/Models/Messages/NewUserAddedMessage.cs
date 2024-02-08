using EmailService.Models.Enums;

namespace EmailService.Models.Messages
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
