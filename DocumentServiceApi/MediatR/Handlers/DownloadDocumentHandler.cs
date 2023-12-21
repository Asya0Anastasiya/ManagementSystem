using DocumentServiceApi.Interfaces.Services;
using DocumentServiceApi.MediatR.Queries;
using DocumentServiceApi.Models.Dto;
using MediatR;

namespace DocumentServiceApi.MediatR.Handlers
{
    public class DownloadDocumentHandler : IRequestHandler<DownloadDocumentQuery, DocumentDto>
    {
        private readonly IDocumentService _documentService;

        public DownloadDocumentHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<DocumentDto> Handle(DownloadDocumentQuery request, CancellationToken cancellationToken)
        {
            return await _documentService.DownloadDocumentAsync(request.FileName, request.UserId);
        }
    }
}
