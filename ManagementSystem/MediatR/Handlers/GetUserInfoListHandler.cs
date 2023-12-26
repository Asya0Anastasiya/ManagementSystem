using MediatR;
using UserService.Interfaces.Services;
using UserService.MediatR.Queries;
using UserService.Models.UserDto;

namespace UserService.MediatR.Handlers
{
    public class GetUserInfoListHandler : IRequestHandler<GetUserInfoListQuery, List<UserInfoModel>>
    {
        private readonly IUserService _userService;

        public GetUserInfoListHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<UserInfoModel>> Handle(GetUserInfoListQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUsersAsync(request.FilteringParams, request.PageNumber, request.PageSize);
        }
    }
}
