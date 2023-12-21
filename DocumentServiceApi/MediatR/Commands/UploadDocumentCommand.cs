using DocumentServiceApi.Models.Dto;
using MediatR;

namespace DocumentServiceApi.MediatR.Commands
{
    public class UploadDocumentCommand : IRequest
    {
        public UploadDocument UploadDocument { get; }

        public UploadDocumentCommand(UploadDocument uploadDocument)
        {
            UploadDocument = uploadDocument;
        }
    }
}
