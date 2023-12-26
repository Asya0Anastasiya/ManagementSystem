using MediatR;
using TimeTrackingService.Models.Dto;

namespace TimeTrackingService.MediatR.Commands
{
    public class AddDaysRangeCommand : IRequest
    {
        public List<CreateDayModel> Days { get; }

        public AddDaysRangeCommand(List<CreateDayModel> days)
        {
            Days = days;
        }
    }
}
