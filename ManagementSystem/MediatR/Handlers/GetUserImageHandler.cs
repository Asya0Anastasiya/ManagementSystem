using MediatR;
using UserService.Interfaces.Services;
using UserService.MediatR.Queries;

namespace UserService.MediatR.Handlers
{
    public class GetUserImageHandler : IRequestHandler<GetUserImageQuery, byte[]>
    {
        private readonly IUserService _userService;

        public GetUserImageHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<byte[]> Handle(GetUserImageQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserImageAsync(request.Id);
        }
    }
}
