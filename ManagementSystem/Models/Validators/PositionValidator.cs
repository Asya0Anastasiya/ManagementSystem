using FluentValidation;
using UserService.Models.Entities;

namespace UserService.Models.Validators
{
    public class PositionValidator : AbstractValidator<PositionEntity>
    {
        public PositionValidator()
        {
            RuleFor(model => model.Id)
                .NotEmpty();

            RuleFor(model => model.Name)
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("Position name can't be empty or more than 30 characters"); ;

            RuleFor(model => model.DepartmentId)
                .NotEmpty();
        }
    }
}
