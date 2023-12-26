using MediatR;
using TimeTrackingService.Models.Dto;

namespace TimeTrackingService.MediatR.Commands
{
    public class AddDayCommand : IRequest
    {
        public CreateDayModel CreateDayModel { get; }

        public AddDayCommand(CreateDayModel createDayModel)
        {
            CreateDayModel = createDayModel;
        }
    }
}
