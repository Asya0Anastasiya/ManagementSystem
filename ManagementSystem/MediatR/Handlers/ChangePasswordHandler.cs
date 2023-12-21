using MediatR;
using UserService.Interfaces.Services;
using UserService.MediatR.Commands;

namespace UserService.MediatR.Handlers
{
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
            return;
        }
    }
}
