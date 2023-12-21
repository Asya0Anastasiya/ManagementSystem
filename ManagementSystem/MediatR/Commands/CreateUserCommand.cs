using MediatR;
using UserService.Models.UserDto;

namespace UserService.MediatR.Commands
{
    public class CreateUserCommand : IRequest
    {
        public SignUpModel SignUpModel { get; }

        public CreateUserCommand(SignUpModel signUpModel)
        {
            SignUpModel = signUpModel;
        }
    }
}
