using MediatR;
using TimeTrackingService.Interfaces.Services;

namespace TimeTrackingService.MediatR.Commands
{
    public class RemoveDayCommand : IRequest
    {
        public Guid Id { get; }

        public RemoveDayCommand(Guid id)
        {
            Id = id;
        }
    }

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
