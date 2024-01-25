using DocumentServiceApi.Interfaces.Services;
using DocumentServiceApi.Models.Dto;
using MediatR;

namespace DocumentServiceApi.MediatR.Queries
{
    public class DownloadDocumentQuery : IRequest<DocumentDto>
    {
        public Guid DocumentId { get; }

        public Guid UserId { get; }

        public DownloadDocumentQuery(Guid userId, Guid documentId)
        {
            UserId = userId;
            DocumentId = documentId;
        }
    }

    public class DownloadDocumentHandler : IRequestHandler<DownloadDocumentQuery, DocumentDto>
    {
        private readonly IDocumentService _documentService;

        public DownloadDocumentHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<DocumentDto> Handle(DownloadDocumentQuery request, CancellationToken cancellationToken)
        {
            return await _documentService.DownloadDocumentAsync(request.DocumentId, request.UserId);
        }
    }
}
