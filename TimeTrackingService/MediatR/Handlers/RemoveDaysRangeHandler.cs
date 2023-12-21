using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.MediatR.Commands;

namespace TimeTrackingService.MediatR.Handlers
{
    public class RemoveDaysRangeHandler : IRequestHandler<RemoveDaysRangeCommand>
    {
        private readonly IDayAccountingService _service;

        public RemoveDaysRangeHandler(IDayAccountingService service)
        {
            _service = service;
        }

        public async Task Handle(RemoveDaysRangeCommand request, CancellationToken cancellationToken)
        {
            await _service.RemoveRangeOfDays(request.Ids);

            return;
        }
    }
}
