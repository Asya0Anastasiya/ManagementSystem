using FluentValidation;
using UserService.Models.Entities;

namespace UserService.Models.Validators
{
    public class BranchOfficeValidator : AbstractValidator<BranchOfficeEntity>
    {
        public BranchOfficeValidator()
        {
            RuleFor(model => model.Id)
                .NotEmpty();

            RuleFor(model => model.Name)
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("Branch office name can't be empty or more than 30 characters"); ;

            RuleFor(model => model.AddressId).NotEmpty();
        }
    }
}
