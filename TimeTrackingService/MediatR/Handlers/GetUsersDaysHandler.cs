using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.MediatR.Queries;
using TimeTrackingService.Models.Dto;

namespace TimeTrackingService.MediatR.Handlers
{
    public class GetUsersDaysHandler : IRequestHandler<GetUsersDaysQuery, List<DayAccountingModel>>
    {
        private readonly IDayAccountingService _service;

        public GetUsersDaysHandler(IDayAccountingService service)
        {
            _service = service;
        }

        public async Task<List<DayAccountingModel>> Handle(GetUsersDaysQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetUsersDays(request.Parameters, request.PageNumber, request.PageSize);
        }
    }
}
