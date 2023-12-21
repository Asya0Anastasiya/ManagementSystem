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
}
