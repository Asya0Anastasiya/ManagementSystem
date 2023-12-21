using MediatR;

namespace TimeTrackingService.MediatR.Queries
{
    public class GetAllUsersTimeTrackDocsNamesQuery : IRequest<List<string>>
    {
        public Guid UserId { get; }

        public GetAllUsersTimeTrackDocsNamesQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
