using FluentValidation;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Models.Validators
{
    public class DayAccountingValidator : AbstractValidator<DayAccounting>
    {
        public DayAccountingValidator()
        {
            RuleFor(model => model.Id).NotEmpty();

            RuleFor(model => model.Hours).NotEmpty().InclusiveBetween(1, 12);

            RuleFor(model => model.Day).NotEmpty().InclusiveBetween(1, 31);

            RuleFor(model => model.Month).NotEmpty().InclusiveBetween(1, 12);

            RuleFor(model => model.Year).NotEmpty().LessThanOrEqualTo(DateTime.Now.Year);

            RuleFor(model => model.Date).NotEmpty();

            RuleFor(model => model.AccountingType).NotEmpty().IsInEnum();

            RuleFor(model => model.IsConfirmed).NotEmpty();

            RuleFor(model => model.UserId).NotEmpty();
        }
    }
}
