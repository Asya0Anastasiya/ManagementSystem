using FluentValidation;
using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;

namespace TimeTrackingService.MediatR.Commands
{
    public class AddDayCommand : IRequest
    {
        public CreateDayModel CreateDayModel { get; }

        public AddDayCommand(CreateDayModel createDayModel)
        {
            CreateDayModel = createDayModel;
        }
    }

    public class AddDayCommandValidator : AbstractValidator<AddDayCommand>
    {
        public AddDayCommandValidator()
        {
            RuleFor(model => model.CreateDayModel.Hours).NotEmpty();

            RuleFor(model => model.CreateDayModel.Date).NotEmpty();

            RuleFor(model => model.CreateDayModel.AccountingType).NotEmpty();

            RuleFor(model => model.CreateDayModel.UserId).NotEmpty();

            RuleFor(model => model.CreateDayModel.IsConfirmed).NotEmpty();
        }
    }

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
