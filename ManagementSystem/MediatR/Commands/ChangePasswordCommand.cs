using FluentValidation;
using MediatR;
using UserService.Helpers;
using UserService.Interfaces.Services;
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

    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(model => model.ChangePasswordModel.Id)
                .NotEmpty()
                .WithMessage("Invalid user Id");

            RuleFor(model => model.ChangePasswordModel.OldPassword)
                .NotEmpty();

            RuleFor(model => model.ChangePasswordModel.NewPassword)
                .NotEmpty()
                .PasswordValidator();
        }
    }

    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IUserService _userService;

        public ChangePasswordHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            await _userService.ChangePassword(request.ChangePasswordModel.Id,
                                            request.ChangePasswordModel.OldPassword,
                                            request.ChangePasswordModel.NewPassword);
        }
    }
}
