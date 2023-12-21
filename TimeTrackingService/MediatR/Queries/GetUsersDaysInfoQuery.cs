using MediatR;
using TimeTrackingService.Models.Dto;

namespace TimeTrackingService.MediatR.Queries
{
    public class GetUsersDaysInfoQuery : IRequest<UsersDaysModel>
    {
        public Guid UserId {  get; }
        public int Month {  get; }
        public int Year { get; }

        public GetUsersDaysInfoQuery(Guid userId, int month, int year)
        {
            UserId = userId;
            Month = month;
            Year = year;
        }

    }
}
