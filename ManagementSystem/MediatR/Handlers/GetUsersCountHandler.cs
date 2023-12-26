using MediatR;
using UserService.Interfaces.Services;
using UserService.MediatR.Queries;

namespace UserService.MediatR.Handlers
{
    public class GetUsersCountHandler : IRequestHandler<GetUsersCountQuery, int>
    {
        private readonly IUserService _userService;

        public GetUsersCountHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<int> Handle(GetUsersCountQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUsersCountAsync();
        }
    }
}
