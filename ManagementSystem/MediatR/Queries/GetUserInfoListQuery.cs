using MediatR;
using UserService.Interfaces.Services;
using UserService.Models.Params;
using UserService.Models.UserDTO;

namespace UserService.MediatR.Queries
{
    public class GetUserInfoListQuery : IRequest<UsersListModel>
    {
        public FilteringParameters FilteringParams { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetUserInfoListQuery(FilteringParameters parameters, int pageNumber, int pageSize)
        {
            FilteringParams = parameters;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetUserInfoListHandler : IRequestHandler<GetUserInfoListQuery, UsersListModel>
    {
        private readonly IUserService _userService;

        public GetUserInfoListHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UsersListModel> Handle(GetUserInfoListQuery request, CancellationToken cancellationToken)
        {
            var usersListModel = new UsersListModel()
            {
                UserInfoModels = await _userService.GetUsersAsync(request.FilteringParams, request.PageNumber, request.PageSize),
                UsersCount = await _userService.GetUsersCountAsync()
            };

            return usersListModel;
        }
    }
}
