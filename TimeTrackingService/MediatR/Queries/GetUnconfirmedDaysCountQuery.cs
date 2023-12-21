using MediatR;

namespace TimeTrackingService.MediatR.Queries
{
    public class GetUnconfirmedDaysCountQuery : IRequest<int>
    {
        public Guid UserId { get; }

        public GetUnconfirmedDaysCountQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
