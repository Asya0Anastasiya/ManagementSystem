using FluentValidation;
using UserService.Models.Entities;

namespace UserService.Models.Validators
{
    public class DepartmentValidator : AbstractValidator<DepartmentEntity>
    {
        public DepartmentValidator()
        {
            RuleFor(model => model.Id).NotEmpty();

            RuleFor(model => model.Name).NotEmpty().MaximumLength(30);

            RuleFor(model => model.BranchOfficeId).NotEmpty();
        }
    }
}
