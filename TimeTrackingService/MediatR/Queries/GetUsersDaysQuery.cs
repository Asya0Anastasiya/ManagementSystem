using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Params;

namespace TimeTrackingService.MediatR.Queries
{
    public class GetUsersDaysQuery : IRequest<List<DayAccountingModel>>
    {
        public FilteringParameters Parameters {  get; }

        public GetUsersDaysQuery(FilteringParameters parameters)
        {
            Parameters = parameters;
        }
    }

    public class GetUsersDaysHandler : IRequestHandler<GetUsersDaysQuery, List<DayAccountingModel>>
    {
        private readonly IDayAccountingService _service;

        public GetUsersDaysHandler(IDayAccountingService service)
        {
            _service = service;
        }

        public async Task<List<DayAccountingModel>> Handle(GetUsersDaysQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetUsersDays(request.Parameters);
        }
    }
}
