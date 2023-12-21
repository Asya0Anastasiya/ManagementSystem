using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.MediatR.Commands;

namespace TimeTrackingService.MediatR.Handlers
{
    public class RemoveDayHandler : IRequestHandler<RemoveDayCommand>
    {
        private readonly IDayAccountingService _service;

        public RemoveDayHandler(IDayAccountingService service)
        {
            _service = service;
        }

        public async Task Handle(RemoveDayCommand request, CancellationToken cancellationToken)
        {
            await _service.RemoveDayAsync(request.Id);

            return;
        }
    }
}
