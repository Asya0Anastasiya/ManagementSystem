using MediatR;
using UserService.Models.UserDto;

namespace UserService.MediatR.Commands
{
    public class UpdateUserCommand : IRequest
    {
        public UpdateUserModel UpdateUserModel { get; }

        public UpdateUserCommand(UpdateUserModel updateUserModel)
        {
            UpdateUserModel = updateUserModel;
        }
    }
}
