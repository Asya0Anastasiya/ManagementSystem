using FluentValidation;
using UserService.Models.Entities;

namespace UserService.Models.Validators
{
    public class UserValidator : AbstractValidator<UserEntity>
    {
        public UserValidator() 
        {
            RuleFor(model => model.Id)
                .NotEmpty()
                .WithMessage("Invalid user Id");

            RuleFor(model => model.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Firstname can't be empty or more than 50 characters");

            RuleFor(model => model.LastName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Lastname can't be empty or more than 50 characters"); ;

            RuleFor(model => model.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(50)
                .WithMessage("Email can't be empty or more than 50 characters"); ;

            RuleFor(model => model.PositionId)
                .NotEmpty();

            RuleFor(model => model.Role)
                .NotEmpty()
                .IsInEnum();

            RuleFor(model => model.PhoneNumber)
                .MaximumLength(15)
                .WithMessage("Phone number can't be more than 50 characters"); ;

            RuleFor(model => model.Password)
                .NotEmpty()
                .MaximumLength(65)
                .WithMessage("Password can't be empty or more than 65 characters"); ;
        }
    }
}
