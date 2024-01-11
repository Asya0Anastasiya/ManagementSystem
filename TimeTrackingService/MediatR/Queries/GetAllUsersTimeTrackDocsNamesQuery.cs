using MediatR;
using TimeTrackingService.Interfaces.Services;

namespace TimeTrackingService.MediatR.Queries
{
    public class GetAllUsersTimeTrackDocsNamesQuery : IRequest<List<string>>
    {
        public Guid UserId { get; }

        public GetAllUsersTimeTrackDocsNamesQuery(Guid userId)
        {
            UserId = userId;
        }
    }

    public class GetAllUsersTimeTrackDocsNamesHandler : IRequestHandler<GetAllUsersTimeTrackDocsNamesQuery, List<string>>
    {
        private readonly IDocumentService _documentService;

        public GetAllUsersTimeTrackDocsNamesHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<List<string>> Handle(GetAllUsersTimeTrackDocsNamesQuery request, CancellationToken cancellationToken)
        {
            return await _documentService.GetAllUsersTimeTrackDocsNames(request.UserId);
        }
    }
}
