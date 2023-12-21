using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.MediatR.Commands;

namespace TimeTrackingService.MediatR.Handlers
{
    public class ApproveDayHandler : IRequestHandler<ApproveDayCommand>
    {
        private readonly IDayAccountingService _service;

        public ApproveDayHandler(IDayAccountingService service)
        {
            _service = service;
        }

        public async Task Handle(ApproveDayCommand request, CancellationToken cancellationToken)
        {
            await _service.ApproveDayAsync(request.Id);

            return;
        }
    }
}
