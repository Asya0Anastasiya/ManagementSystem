using FluentValidation;
using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;

namespace TimeTrackingService.MediatR.Commands
{
    public class AttachDocumentCommand : IRequest
    {
        public Guid UserId { get; }
        public AttachDocModel AttachDocModel { get; }

        public AttachDocumentCommand(Guid userId, AttachDocModel attachDocModel)
        {
            UserId = userId;
            AttachDocModel = attachDocModel;
        }
    }

    public class AttachDocumentCommandValidator : AbstractValidator<AttachDocumentCommand>
    {
        public AttachDocumentCommandValidator()
        {
            RuleFor(model => model.AttachDocModel.Date)
                .NotEmpty();
        }
    }

    public class AttachDocumentHandler : IRequestHandler<AttachDocumentCommand>
    {
        private readonly IDocumentService _documentService;

        public AttachDocumentHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task Handle(AttachDocumentCommand request, CancellationToken cancellationToken)
        {
            await _documentService.AttachDocumentToDay(request.AttachDocModel.DocumentId, request.AttachDocModel.Date, request.UserId);
        }
    }
}
