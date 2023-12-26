using DocumentServiceApi.Models.Dto;
using MediatR;

namespace DocumentServiceApi.MediatR.Queries
{
    public class DownloadDocumentQuery : IRequest<DocumentDto>
    {
        public string FileName { get; }
        public Guid UserId { get; }

        public DownloadDocumentQuery(Guid userId, string fileName)
        {
            UserId = userId;
            FileName = fileName;
        }
    }
}
