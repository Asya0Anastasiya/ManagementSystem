using MediatR;
using TimeTrackingService.Models.Dto;

namespace TimeTrackingService.MediatR.Commands
{
    public class AttachDocumentCommand : IRequest
    {
        public Guid UserId { get; }
        public AttachDocModel AttachDocModel { get; }

        public AttachDocumentCommand(Guid userId, AttachDocModel attachDocModel)
        {
            UserId = userId;
            AttachDocModel = attachDocModel;
        }

    }
}
