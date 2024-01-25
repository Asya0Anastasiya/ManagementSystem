using MediatR;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;

namespace TimeTrackingService.MediatR.Queries
{
    public class GetUsersDocumentsNamesForAdminQuery : IRequest<List<DocumentWithSourceIdModel>>
    {
        public Guid UserId {  get; }
        public DateTime DateTime { get; }

        public GetUsersDocumentsNamesForAdminQuery(Guid userId, DateTime dateTime)
        {
            UserId = userId;
            DateTime = dateTime;
        }
    }

    public class GetUsersDocumentsNamesForAdminHandler : IRequestHandler<GetUsersDocumentsNamesForAdminQuery, List<DocumentWithSourceIdModel>>
    {
        private readonly IDocumentService _documentService;

        public GetUsersDocumentsNamesForAdminHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<List<DocumentWithSourceIdModel>> Handle(GetUsersDocumentsNamesForAdminQuery request, CancellationToken cancellationToken)
        {
            return await _documentService.GetAttachedUsersDocumentsNames(request.UserId, request.DateTime);
        }
    }
}
