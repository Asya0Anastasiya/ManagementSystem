using FluentValidation;
using UserService.Models.Entities;

namespace UserService.Models.Validators
{
    public class PositionValidator : AbstractValidator<PositionEntity>
    {
        public PositionValidator()
        {
            RuleFor(model => model.Id).NotEmpty();

            RuleFor(model => model.Name).NotEmpty().MaximumLength(30);

            RuleFor(model => model.DepartmentId).NotEmpty();
        }
    }
}
