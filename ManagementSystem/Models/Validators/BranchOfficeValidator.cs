using FluentValidation;
using UserService.Models.Entities;

namespace UserService.Models.Validators
{
    public class BranchOfficeValidator : AbstractValidator<BranchOfficeEntity>
    {
        public BranchOfficeValidator()
        {
            RuleFor(model => model.Id).NotEmpty();

            RuleFor(model => model.Name).NotEmpty().MaximumLength(30);

            RuleFor(model => model.AddressId).NotEmpty();
        }
    }
}
