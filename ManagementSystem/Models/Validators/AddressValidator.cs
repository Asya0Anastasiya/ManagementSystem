using FluentValidation;
using UserService.Models.Entities;

namespace UserService.Models.Validators
{
    public class AddressValidator : AbstractValidator<AddressEntity>
    {
        public AddressValidator()
        {
            RuleFor(model => model.Id)
                .NotEmpty();

            RuleFor(model => model.Country)
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("Country can't be empty or more than 30 characters"); ;

            RuleFor(model => model.City)
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("City can't be empty or more than 30 characters"); ;

            RuleFor(model => model.Street)
                .NotEmpty()
                .MaximumLength(35)
                .WithMessage("Street can't be empty or more than 35 characters"); ;

            RuleFor(model => model.Building)
                .NotEmpty()
                .MaximumLength(10)
                .WithMessage("Building can't be empty or more than 10 characters"); ;
        }
    }
}
