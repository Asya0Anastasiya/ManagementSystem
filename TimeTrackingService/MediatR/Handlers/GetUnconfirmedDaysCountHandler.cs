using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.MediatR.Queries;

namespace TimeTrackingService.MediatR.Handlers
{
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
