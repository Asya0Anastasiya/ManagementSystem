using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Entities;
using TimeTrackingService.Models.Params;

namespace TimeTrackingService.Interfaces.Services
{
    public interface IDayAccountingService
    {
        public Task AddDay(CreateDayModel dayModel);

        public Task AddRangeOfDays(List<CreateDayModel> days);

        public Task<List<DayAccountingModel>> GetUsersDays(FilteringParameters parameters);

        public Task<int> GetUnconfirmedDaysCount(Guid id);

        public Task RemoveDayAsync(Guid id);

        public Task ApproveDayAsync(Guid id);

        public Task<DayAccounting> GetUserDay(Guid userId, DateTime date);

        public Task<UsersDaysModel> GetUsersDaysInfo(Guid id, int month, int year);
    }
}
