using FluentValidation;
using MediatR;
using UserService.Interfaces.Services;

namespace UserService.MediatR.Commands
{
    public class SetUserImageCommand : IRequest
    {
        public Guid UserId { get; }

        public IFormFile File { get; }

        public SetUserImageCommand(Guid userId, IFormFile file)
        {
            UserId = userId;
            File = file;
        }
    }

    public class SetUserImageCommandValidator : AbstractValidator<SetUserImageCommand>
    {
        public SetUserImageCommandValidator()
        {
            RuleFor(model => model.UserId)
                .NotEmpty()
                .WithMessage("Invalid user Id");

            RuleFor(model => model.File)
                .NotEmpty()
                .WithMessage("Invalid file");
        }
    }

    public class SetUserImageHandler : IRequestHandler<SetUserImageCommand>
    {
        private readonly IUserService _userService;

        public SetUserImageHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(SetUserImageCommand request, CancellationToken cancellationToken)
        {
            await _userService.SetUserImageAsync(request.UserId, request.File);
        }
    }
}
