using FluentValidation;
using MediatR;
using UserService.Interfaces.Services;
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

    public class CreateUserCommandValidator : AbstractValidator<SignUpModel>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(model => model.Firstname).NotEmpty().MaximumLength(50);

            RuleFor(model => model.Lastname).NotEmpty().MaximumLength(50);

            RuleFor(model => model.Email).NotEmpty().MaximumLength(50).EmailAddress();

            RuleFor(model => model.Password).NotEmpty().MaximumLength(25);

            RuleFor(model => model.PositionId).NotEmpty();

            RuleFor(model => model.PhoneNumber).NotEmpty().MaximumLength(15);
        }
    }

    public class CreateUserHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _userService.Create(request.SignUpModel);

            return;
        }
    }
}
