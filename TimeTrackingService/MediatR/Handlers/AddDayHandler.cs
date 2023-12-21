using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.MediatR.Commands;

namespace TimeTrackingService.MediatR.Handlers
{
    public class AddDayHandler : IRequestHandler<AddDayCommand>
    {
        private readonly IDayAccountingService _accountingService;

        public AddDayHandler(IDayAccountingService accountingService)
        {
            _accountingService = accountingService;
        }

        public async Task Handle(AddDayCommand request, CancellationToken cancellationToken)
        {
            await _accountingService.AddDay(request.CreateDayModel);
        }
    }
}
