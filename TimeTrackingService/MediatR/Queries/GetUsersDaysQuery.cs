using MediatR;
using TimeTrackingService.Helpers.Filtering;
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
}
