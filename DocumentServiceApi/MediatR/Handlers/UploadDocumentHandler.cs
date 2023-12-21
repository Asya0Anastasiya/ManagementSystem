using DocumentServiceApi.Interfaces.Services;
using DocumentServiceApi.MediatR.Commands;
using MediatR;

namespace DocumentServiceApi.MediatR.Handlers
{
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

            return;
        }
    }
}
