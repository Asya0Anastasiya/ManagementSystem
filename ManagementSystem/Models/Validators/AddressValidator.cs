using FluentValidation;
using UserService.Models.Entities;

namespace UserService.Models.Validators
{
    public class AddressValidator : AbstractValidator<AddressEntity>
    {
        public AddressValidator()
        {
            RuleFor(model => model.Id).NotEmpty();

            RuleFor(model => model.Country).NotEmpty().MaximumLength(30);

            RuleFor(model => model.City).NotEmpty().MaximumLength(30);

            RuleFor(model => model.Street).NotEmpty().MaximumLength(35);

            RuleFor(model => model.Building).NotEmpty().MaximumLength(10);
        }
    }
}
