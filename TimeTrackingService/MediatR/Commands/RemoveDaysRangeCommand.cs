using MediatR;
using TimeTrackingService.Interfaces.Services;

namespace TimeTrackingService.MediatR.Commands
{
    public class RemoveDaysRangeCommand : IRequest
    {
        public List<Guid> Ids { get; }

        public RemoveDaysRangeCommand(List<Guid> ids)
        {
            Ids = ids;
        }
    }

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
