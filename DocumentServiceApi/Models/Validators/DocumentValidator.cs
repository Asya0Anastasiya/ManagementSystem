using DocumentServiceApi.Models.Entities;
using FluentValidation;

namespace DocumentServiceApi.Models.Validators
{
    public class DocumentValidator : AbstractValidator<DocumentEntity>
    {
        public DocumentValidator()
        {
            RuleFor(model => model.Id).NotEmpty();

            RuleFor(model => model.Name).NotEmpty().MaximumLength(60);

            RuleFor(model => model.Size).NotEmpty().InclusiveBetween(0, 10000);

            RuleFor(model => model.ContentType).NotEmpty().MaximumLength(20);

            RuleFor(model => model.Type).NotEmpty().IsInEnum();

            RuleFor(model => model.UploadDate).NotEmpty();

            RuleFor(model => model.UserId).NotEmpty();
        }
    }
}
