using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;

namespace TimeTrackingService.MediatR.Queries
{
    public class GetAllUsersTimeTrackDocsNamesQuery : IRequest<List<DocumentInfoModel>>
    {
        public Guid UserId { get; }

        public GetAllUsersTimeTrackDocsNamesQuery(Guid userId)
        {
            UserId = userId;
        }
    }

    public class GetAllUsersTimeTrackDocsNamesHandler : IRequestHandler<GetAllUsersTimeTrackDocsNamesQuery, List<DocumentInfoModel>>
    {
        private readonly IDocumentService _documentService;

        public GetAllUsersTimeTrackDocsNamesHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<List<DocumentInfoModel>> Handle(GetAllUsersTimeTrackDocsNamesQuery request, CancellationToken cancellationToken)
        {
            return await _documentService.GetAllUsersTimeTrackDocsNames(request.UserId);
        }
    }
}
