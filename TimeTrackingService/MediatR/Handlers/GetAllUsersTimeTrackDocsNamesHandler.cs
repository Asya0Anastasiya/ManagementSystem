using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.MediatR.Queries;

namespace TimeTrackingService.MediatR.Handlers
{
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
