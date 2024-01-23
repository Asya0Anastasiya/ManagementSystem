using MediatR;
using TimeTrackingService.Helpers.Filtering;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;

namespace TimeTrackingService.MediatR.Queries
{
    public class GetUsersDaysQuery : IRequest<List<DayAccountingModel>>
    {
        public FilteringParameters Parameters {  get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetUsersDaysQuery(FilteringParameters parameters, int pageNumber, int pageSize)
        {
            Parameters = parameters;
            PageNumber = pageNumber;
            PageSize = pageSize;
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
            return await _service.GetUsersDays(request.Parameters, request.PageNumber, request.PageSize);
        }
    }
}
