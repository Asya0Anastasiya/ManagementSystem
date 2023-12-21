using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.MediatR.Queries;
using TimeTrackingService.Models.Dto;

namespace TimeTrackingService.MediatR.Handlers
{
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
