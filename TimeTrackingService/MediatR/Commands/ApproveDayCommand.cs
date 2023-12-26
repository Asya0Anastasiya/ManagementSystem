using MediatR;

namespace TimeTrackingService.MediatR.Commands
{
    public class ApproveDayCommand : IRequest
    {
        public Guid Id { get; }

        public ApproveDayCommand(Guid id)
        {
            Id = id;
        }
    }
}
