using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.MediatR.Commands;

namespace TimeTrackingService.MediatR.Handlers
{
    public class AddDaysRangeHandler : IRequestHandler<AddDaysRangeCommand>
    {
        private readonly IDayAccountingService _service;

        public AddDaysRangeHandler(IDayAccountingService service)
        {
            _service = service;
        }

        public async Task Handle(AddDaysRangeCommand request, CancellationToken cancellationToken)
        {
            await _service.AddRangeOfDays(request.Days);
        }
    }
}
