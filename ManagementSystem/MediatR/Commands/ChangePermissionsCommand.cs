using FluentValidation;
using MediatR;
using UserService.Interfaces.Services;
using UserService.Models.Enums;
using UserService.Models.UserDTO;

namespace UserService.MediatR.Commands
{
    public class ChangePermissionsCommand : IRequest
    {
        public ChangePermissionsModel PermissionsModel { get; }

        public ChangePermissionsCommand(ChangePermissionsModel permissionsModel)
        {
            PermissionsModel = permissionsModel;
        }
    }

    public class ChangePermissionsCommandValidator : AbstractValidator<ChangePermissionsCommand>
    {
        public ChangePermissionsCommandValidator()
        {
            RuleFor(model => model.PermissionsModel.UserId)
                .NotEmpty()
                .WithMessage("Invalid id");

            RuleFor(model => model.PermissionsModel.AdminId)
                .NotEmpty()
                .WithMessage("Invalid id");

            RuleFor(model => model.PermissionsModel.NewRole)
                .NotEmpty()
                .LessThanOrEqualTo(Enum.GetNames(typeof(Roles)).Length)
                .GreaterThan(0)
                .WithMessage("Invalid role");
        }
    }

    public class ChangePermissionsHandler : IRequestHandler<ChangePermissionsCommand>
    {
        private readonly IUserService _userService;

        public ChangePermissionsHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(ChangePermissionsCommand request, CancellationToken cancellationToken)
        {
            await _userService.ChangeUserPermissionsAsync(request.PermissionsModel);
        }
    }
}
