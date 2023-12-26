using MediatR;
using UserService.Models.UserDto;

namespace UserService.MediatR.Commands
{
    public class ChangePasswordCommand : IRequest
    {
        public ChangePasswordModel ChangePasswordModel { get; }

        public ChangePasswordCommand(ChangePasswordModel changePasswordModel)
        {
            ChangePasswordModel = changePasswordModel;
        }
    }
}
