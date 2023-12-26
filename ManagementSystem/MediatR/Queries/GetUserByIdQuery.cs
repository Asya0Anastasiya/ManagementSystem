using MediatR;
using UserService.Models.UserDto;

namespace UserService.MediatR.Queries
{
    public class GetUserByIdQuery : IRequest<UserInfoModel>
    {
        public Guid Id { get; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
