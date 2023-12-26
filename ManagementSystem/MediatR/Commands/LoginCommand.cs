using MediatR;
using UserService.Models.UserDto;

namespace UserService.MediatR.Commands
{
    public class LoginCommand : IRequest<string>
    {
        public SignInModel SignInModel { get; }

        public LoginCommand(SignInModel signInModel)
        {
            SignInModel = signInModel;
        }
    }
}
