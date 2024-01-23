using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;

namespace TimeTrackingService.MediatR.Commands
{
    public class AddDaysRangeCommand : IRequest
    {
        public List<CreateDayModel> Days { get; }

        public AddDaysRangeCommand(List<CreateDayModel> days)
        {
            Days = days;
        }
    }

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
