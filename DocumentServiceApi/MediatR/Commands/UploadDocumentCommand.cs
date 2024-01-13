using DocumentServiceApi.Interfaces.Services;
using DocumentServiceApi.Models.Dto;
using FluentValidation;
using MediatR;

namespace DocumentServiceApi.MediatR.Commands
{
    public class UploadDocumentCommand : IRequest
    {
        public UploadDocument UploadDocument { get; }

        public UploadDocumentCommand(UploadDocument uploadDocument)
        {
            UploadDocument = uploadDocument;
        }
    }

    public class UploadDocumentCommandValidator : AbstractValidator<UploadDocumentCommand>
    {
        public UploadDocumentCommandValidator()
        {
            RuleFor(model => model.UploadDocument.UserId)
                .NotEmpty()
                .WithMessage("Invalid user Id");

            RuleFor(model => model.UploadDocument.Type)
                .NotEmpty()
                .IsInEnum()
                .WithMessage("Invalid document Type");

            RuleFor(model => model.UploadDocument.File)
                .NotEmpty()
                .WithMessage("Invalid file");
        }
    }

    public class UploadDocumentHandler : IRequestHandler<UploadDocumentCommand>
    {
        private readonly IDocumentService _documentService;

        public UploadDocumentHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
        {
            await _documentService.UploadDocumentAsync(request.UploadDocument);
        }
    }
}
