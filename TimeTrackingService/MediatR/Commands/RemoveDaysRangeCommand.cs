using MediatR;

namespace TimeTrackingService.MediatR.Commands
{
    public class RemoveDaysRangeCommand : IRequest
    {
        public List<Guid> Ids { get; }

        public RemoveDaysRangeCommand(List<Guid> ids)
        {
            Ids = ids;
        }
    }
}
