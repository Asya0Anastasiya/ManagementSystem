using FluentValidation;
using MediatR;
using UserService.Interfaces.Services;
using UserService.Models.UserDTO;

namespace UserService.MediatR.Commands
{
    public class ChangePositionCommand : IRequest
    {
        public ChangePositionModel PositionModel { get; }

        public ChangePositionCommand(ChangePositionModel positionModel)
        {
            PositionModel = positionModel;
        }
    }

    public class ChangePositionCommandValidator : AbstractValidator<ChangePositionCommand>
    {
        public ChangePositionCommandValidator()
        {
            RuleFor(model => model.PositionModel.UserId)
                .NotEmpty()
                .WithMessage("Invalid user");
            
            RuleFor(model => model.PositionModel.AdminId)
                .NotEmpty()
                .WithMessage("Invalid user");
            
            RuleFor(model => model.PositionModel.PositionId)
                .NotEmpty()
                .WithMessage("Invalid position");
        }
    }

    public class ChangePositionHandler : IRequestHandler<ChangePositionCommand>
    {
        private readonly IUserService _userService;

        public ChangePositionHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(ChangePositionCommand request, CancellationToken cancellationToken)
        {
            await _userService.ChangeUserPositionAsync(request.PositionModel);
        }
    }
}
