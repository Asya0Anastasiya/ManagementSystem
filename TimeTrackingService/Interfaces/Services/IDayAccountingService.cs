using TimeTrackingService.Helpers.Filtering;
using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Interfaces.Services
{
    public interface IDayAccountingService
    {
        public Task AddDay(CreateDayModel dayModel);

        public Task AddRangeOfDays(List<CreateDayModel> days);

        public Task<List<DayAccountingModel>> GetUsersDays(FilteringParameters parameters, int pageNumber, int pageSize);

        public Task<int> GetUnconfirmedDaysCount(Guid id);

        public Task RemoveDayAsync(Guid id);

        public Task RemoveRangeOfDays(List<Guid> ids);

        public Task ApproveDayAsync(Guid id);

        public Task<DayAccounting> GetUserDay(Guid userId, DateTime date);

        public Task<UsersDaysModel> GetUsersDaysInfo(Guid id, int month, int year);
    }
}
