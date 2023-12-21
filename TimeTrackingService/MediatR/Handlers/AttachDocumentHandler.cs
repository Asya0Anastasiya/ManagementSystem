using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.MediatR.Commands;

namespace TimeTrackingService.MediatR.Handlers
{
    public class AttachDocumentHandler : IRequestHandler<AttachDocumentCommand>
    {
        private readonly IDocumentService _documentService;

        public AttachDocumentHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task Handle(AttachDocumentCommand request, CancellationToken cancellationToken)
        {
            await _documentService.AttachDocumentToDay(request.AttachDocModel.Name, request.AttachDocModel.Date, request.UserId);
        }
    }
}
