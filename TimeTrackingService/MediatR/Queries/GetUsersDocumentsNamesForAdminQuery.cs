using MediatR;
using TimeTrackingService.Interfaces.Services;

namespace TimeTrackingService.MediatR.Queries
{
    public class GetUsersDocumentsNamesForAdminQuery : IRequest<List<string>>
    {
        public Guid UserId {  get; }
        public DateTime DateTime { get; }

        public GetUsersDocumentsNamesForAdminQuery(Guid userId, DateTime dateTime)
        {
            UserId = userId;
            DateTime = dateTime;
        }
    }

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
