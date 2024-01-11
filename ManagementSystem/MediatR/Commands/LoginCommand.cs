using FluentValidation;
using MediatR;
using UserService.Interfaces.Services;
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

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(model => model.SignInModel.Email).NotEmpty().EmailAddress().MaximumLength(50);

            RuleFor(model => model.SignInModel.Password).NotEmpty().MaximumLength(25);
        }
    }

    public class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserService _userService;

        public LoginHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _userService.Login(request.SignInModel);
        }
    }
}
