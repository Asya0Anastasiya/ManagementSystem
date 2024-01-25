using DocumentServiceApi.Models.Entities;
using FluentValidation;

namespace DocumentServiceApi.Models.Validators
{
    public class DocumentValidator : AbstractValidator<DocumentEntity>
    {
        public DocumentValidator()
        {
            RuleFor(model => model.Id)
                .NotEmpty();

            RuleFor(model => model.Name)
                .NotEmpty()
                .MaximumLength(60)
                .WithMessage("Document name can't be empty or more than 60 characters");

            RuleFor(model => model.Size)
                .NotEmpty()
                .InclusiveBetween(0, 10000)
                .WithMessage("Document size can't be more than 10 Mb");

            RuleFor(model => model.ContentType)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Content type can't be empty or more than 20 characters");

            RuleFor(model => model.Type)
                .NotEmpty()
                .IsInEnum()
                .WithMessage("Invalid document type");

            RuleFor(model => model.UserId)
                .NotEmpty()
                .WithMessage("Invalid user Id");
        }
    }
}
