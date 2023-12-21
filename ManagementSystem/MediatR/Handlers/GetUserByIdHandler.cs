using MediatR;
using UserService.Interfaces.Services;
using UserService.MediatR.Queries;
using UserService.Models.UserDto;

namespace UserService.MediatR.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserInfoModel>
    {
        private readonly IUserService _userService;

        public GetUserByIdHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserInfoModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserInfo(request.Id);
        }
    }
}
