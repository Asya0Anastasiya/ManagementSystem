using FluentValidation;
using MediatR;
using UserService.Interfaces.Services;
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

    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(model => model.UpdateUserModel.Id)
                .NotEmpty()
                .WithMessage("Invalid user Id");

            RuleFor(model => model.UpdateUserModel.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Firstname can not be empty or contain more than 50 characters");

            RuleFor(model => model.UpdateUserModel.LastName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Lastname can not be empty or contain more than 50 characters");

            RuleFor(model => model.UpdateUserModel.Email)
                .NotEmpty()
                .MaximumLength(50)
                .EmailAddress()
                .WithMessage("Email can not be empty or contain more than 50 characters");

            RuleFor(model => model.UpdateUserModel.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Phone number can not be empty or contain more than 50 characters");
        }
    }

    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserService _userService;

        public UpdateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await _userService.UpdateUserAsync(request.UpdateUserModel);
        }
    }
}
