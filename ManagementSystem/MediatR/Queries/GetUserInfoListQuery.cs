using MediatR;
using UserService.Helpers;
using UserService.Models.UserDto;

namespace UserService.MediatR.Queries
{
    public class GetUserInfoListQuery : IRequest<List<UserInfoModel>>
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
}
