using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.MediatR.Queries;

namespace TimeTrackingService.MediatR.Handlers
{
    public class GetUsersDocumentsNamesForAdminHandler : IRequestHandler<GetUsersDocumentsNamesForAdminQuery, List<string>>
    {
        private readonly IDocumentService _documentService;

        public GetUsersDocumentsNamesForAdminHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<List<string>> Handle(GetUsersDocumentsNamesForAdminQuery request, CancellationToken cancellationToken)
        {
            return await _documentService.GetAttachedUsersDocumentsNames(request.UserId, request.DateTime);
        }
    }
}
