using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;

namespace TimeTrackingService.MediatR.Queries
{
    public class GetUsersDaysInfoQuery : IRequest<UsersDaysModel>
    {
        public Guid UserId {  get; }
        public int Month {  get; }
        public int Year { get; }

        public GetUsersDaysInfoQuery(Guid userId, int month, int year)
        {
            UserId = userId;
            Month = month;
            Year = year;
        }
    }

    public class GetUsersDaysInfoHandler : IRequestHandler<GetUsersDaysInfoQuery, UsersDaysModel>
    {
        private readonly IDayAccountingService _service;

        public GetUsersDaysInfoHandler(IDayAccountingService service)
        {
            _service = service;
        }

        public async Task<UsersDaysModel> Handle(GetUsersDaysInfoQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetUsersDaysInfo(request.UserId, request.Month, request.Year);
        }
    }
}
