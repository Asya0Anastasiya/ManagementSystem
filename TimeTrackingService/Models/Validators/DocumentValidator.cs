using FluentValidation;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Models.Validators
{
    public class DocumentValidator : AbstractValidator<Document>
    {
        public DocumentValidator()
        {
            RuleFor(model => model.Id).NotEmpty();

            RuleFor(model => model.Name).NotEmpty().MaximumLength(50);

            RuleFor(model => model.UserId).NotEmpty();

            RuleFor(model => model.Type).NotEmpty().IsInEnum();

            RuleFor(model => model.SourceId).NotEmpty();
        }
    }
}
