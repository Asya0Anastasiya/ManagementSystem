using TimeTrackingService.Helpers.Pagination;
using TimeTrackingService.Models.Entities;
using TimeTrackingService.Models.Params;

namespace TimeTrackingService.Interfaces.Repositories
{
    public interface IDayAccountingRepository
    {
        public Task AddDay(DayAccounting daysAccounting);

        public Task AddRangeOfDays(List<DayAccounting> daysAccounts);

        public Task<List<DayAccounting>> GetUsersDays(FilteringParameters filtering, PaginationParameters pagination);

        public Task<int> GetUnconfirmedDaysCount(Guid id);

        public Task<DayAccounting> GetDayByIdAsync(Guid id);

        public Task RemoveDayAsync(DayAccounting day);

        public Task UpdateDayAsync(DayAccounting dayAccounting);

        public Task<int> GetUsersWorkDaysCount(Guid id, int month, int year);

        public Task<int> GetUsersSickDaysCount(Guid id, int month, int year);

        public Task<int> GetUsersHolidaysCount(Guid id, int month, int year);

        public Task<DayAccounting> CheckDayForExistenceAsync(DateTime date, Guid userId);
    }
}
