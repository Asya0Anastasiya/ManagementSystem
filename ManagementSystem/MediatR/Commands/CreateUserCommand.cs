using FluentValidation;
using MediatR;
using UserService.Helpers;
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

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(model => model.SignUpModel.Firstname)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Firstname can not be empty or contain more than 50 characters");

            RuleFor(model => model.SignUpModel.Lastname)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Lastname can not be empty or contain more than 50 characters");


            RuleFor(model => model.SignUpModel.Email)
                .NotEmpty()
                .MaximumLength(50)
                .EmailAddress()
                .WithMessage("Email can not be empty or contain more than 50 characters");

            RuleFor(model => model.SignUpModel.Password)
                .NotEmpty()
                .PasswordValidator();

            RuleFor(model => model.SignUpModel.PositionId)
                .NotEmpty();

            RuleFor(model => model.SignUpModel.PhoneNumber)
                .NotEmpty()
                .MaximumLength(15)
                .WithMessage("Phone number can not be empty or contain more than 15 characters");
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
        }
    }
}
