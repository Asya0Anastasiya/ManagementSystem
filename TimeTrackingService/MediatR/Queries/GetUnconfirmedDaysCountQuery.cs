using MediatR;
using TimeTrackingService.Interfaces.Services;

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

    public class GetUnconfirmedDaysCountHandler : IRequestHandler<GetUnconfirmedDaysCountQuery, int>
    {
        private readonly IDayAccountingService _accountingService;

        public GetUnconfirmedDaysCountHandler(IDayAccountingService accountingService)
        {
            _accountingService = accountingService;
        }

        public async Task<int> Handle(GetUnconfirmedDaysCountQuery request, CancellationToken cancellationToken)
        {
            return await _accountingService.GetUnconfirmedDaysCount(request.UserId);
        }
    }
}
