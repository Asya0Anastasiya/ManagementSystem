using FluentValidation;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Models.Validators
{
    public class DayAccountingValidator : AbstractValidator<DayAccounting>
    {
        public DayAccountingValidator()
        {
            RuleFor(model => model.Id)
                .NotEmpty();

            RuleFor(model => model.Hours)
                .NotEmpty()
                .InclusiveBetween(1, 12)
                .WithMessage("Hours must be inclusive between 1 and 12");

            RuleFor(model => model.Day)
                .NotEmpty()
                .InclusiveBetween(1, 31)
                .WithMessage("Day must be inclusive between 1 and 31");

            RuleFor(model => model.Month)
                .NotEmpty()
                .InclusiveBetween(1, 12)
                .WithMessage("Month must be inclusive between 1 and 12");

            RuleFor(model => model.Year)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("Year can't be more than current year");

            RuleFor(model => model.Date)
                .NotEmpty();

            RuleFor(model => model.AccountingType)
                .NotEmpty()
                .IsInEnum()
                .WithMessage("Invalid accounting type");

            RuleFor(model => model.IsConfirmed)
                .NotEmpty();

            RuleFor(model => model.UserId)
                .NotEmpty()
                .WithMessage("Invalid user Id");
        }
    }
}
