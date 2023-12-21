using MediatR;
using UserService.Interfaces.Services;
using UserService.MediatR.Commands;

namespace UserService.MediatR.Handlers
{
    public class CreateUserRequestHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserService _userService;

        public CreateUserRequestHandler(IUserService userService)
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
