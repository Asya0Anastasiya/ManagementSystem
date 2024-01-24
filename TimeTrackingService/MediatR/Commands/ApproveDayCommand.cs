using MediatR;
using TimeTrackingService.Interfaces.Services;

namespace TimeTrackingService.MediatR.Commands
{
    public class ApproveDayCommand : IRequest
    {
        public Guid Id { get; }

        public ApproveDayCommand(Guid id)
        {
            Id = id;
        }
    }

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
        }
    }
}
