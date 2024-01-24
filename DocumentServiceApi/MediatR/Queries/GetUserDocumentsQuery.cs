using DocumentServiceApi.Interfaces.Services;
using DocumentServiceApi.Models.Dto;
using MediatR;

namespace DocumentServiceApi.MediatR.Queries
{
    public class GetUserDocumentsQuery : IRequest<List<DocumentInfo>>
    {
        public Guid UserId { get; }

        public GetUserDocumentsQuery(Guid userId)
        {
            UserId = userId;
        }
    }

    public class GetUserDocumentsHandler : IRequestHandler<GetUserDocumentsQuery, List<DocumentInfo>>
    {
        private readonly IDocumentService _documentService;

        public GetUserDocumentsHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<List<DocumentInfo>> Handle(GetUserDocumentsQuery request, CancellationToken cancellationToken)
        {
            return await _documentService.GetUserDocuments(request.UserId);
        }
    }
}
