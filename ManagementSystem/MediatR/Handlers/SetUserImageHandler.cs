using MediatR;
using UserService.Interfaces.Services;
using UserService.MediatR.Commands;

namespace UserService.MediatR.Handlers
{
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

            return;
        }
    }
}
