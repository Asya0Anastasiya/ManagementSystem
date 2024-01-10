using MediatR;
using UserService.Interfaces.Services;

namespace UserService.MediatR.Queries
{
    public class GetUserImageQuery : IRequest<byte[]>
    {
        public Guid Id { get; }

        public GetUserImageQuery(Guid id)
        {
            Id = id;
        }
    }

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
