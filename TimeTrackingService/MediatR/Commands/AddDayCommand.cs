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
            RuleFor(model => model.CreateDayModel.Hours)
                .NotEmpty()
                .InclusiveBetween(1, 12)
                .WithMessage("Incorrect number of hours");

            RuleFor(model => model.CreateDayModel.Date)
                .NotEmpty()
                .WithMessage("invalid date");

            RuleFor(model => model.CreateDayModel.AccountingType)
                .NotEmpty()
                .WithMessage("Invalid accounting type");

            RuleFor(model => model.CreateDayModel.UserId)
                .NotEmpty()
                .WithMessage("Invalid user Id");

            RuleFor(model => model.CreateDayModel.IsConfirmed)
                .NotEmpty();
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
