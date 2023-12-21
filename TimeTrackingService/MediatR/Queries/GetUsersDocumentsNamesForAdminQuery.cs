using MediatR;

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
}
