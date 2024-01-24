using FluentValidation;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Models.Validators
{
    public class DocumentValidator : AbstractValidator<Document>
    {
        public DocumentValidator()
        {
            RuleFor(model => model.Id)
                .NotEmpty();

            RuleFor(model => model.Name)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Document name can't be empty or more than 50 characters");

            RuleFor(model => model.UserId)
                .NotEmpty()
                .WithMessage("Invalid user Id");

            RuleFor(model => model.Type)
                .NotEmpty()
                .IsInEnum()
                .WithMessage("Invalid document type");

            RuleFor(model => model.SourceId)
                .NotEmpty();
        }
    }
}
