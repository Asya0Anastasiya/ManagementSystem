using MediatR;
using UserService.Interfaces.Services;
using UserService.MediatR.Commands;

namespace UserService.MediatR.Handlers
{
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

            return;
        }
    }
}
