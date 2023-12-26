using MediatR;

namespace TimeTrackingService.MediatR.Commands
{
    public class RemoveDayCommand : IRequest
    {
        public Guid Id { get; }

        public RemoveDayCommand(Guid id)
        {
            Id = id;
        }
    }
}
