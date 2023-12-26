using MediatR;

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
}
