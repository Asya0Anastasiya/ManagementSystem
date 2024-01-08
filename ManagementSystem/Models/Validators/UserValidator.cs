using FluentValidation;
using UserService.Models.Entities;

namespace UserService.Models.Validators
{
    public class UserValidator : AbstractValidator<UserEntity>
    {
        public UserValidator() 
        {
            RuleFor(model => model.Id).NotEmpty();

            RuleFor(model => model.FirstName).NotEmpty().MaximumLength(50);

            RuleFor(model => model.LastName).NotEmpty().MaximumLength(50);

            RuleFor(model => model.Email).NotEmpty().EmailAddress().MaximumLength(50);

            RuleFor(model => model.PositionId).NotEmpty();

            RuleFor(model => model.Role).NotEmpty().IsInEnum();

            RuleFor(model => model.PhoneNumber).MaximumLength(15);

            RuleFor(model => model.Password).NotEmpty().MaximumLength(65);
        }
    }
}
