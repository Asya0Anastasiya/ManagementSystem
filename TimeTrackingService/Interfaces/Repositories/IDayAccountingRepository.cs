using TimeTrackingService.Helpers.Filtering;
using TimeTrackingService.Helpers.Pagination;
using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Entities;

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

        public Task RemoveRangeOfDays(List<Guid> ids);

        public Task ApproveDayAsync(DayAccounting dayAccounting);

        public Task<int> GetUsersWorkDaysCount(Guid id, int month, int year);

        public Task<int> GetUsersSickDaysCount(Guid id, int month, int year);

        public Task<int> GetUsersHolidaysCount(Guid id, int month, int year);

        public Task<int> GetPaidDaysCount(Guid id, int month, int year);

        public Task<UsersDaysModel> GetUsersDaysInfo(Guid id, int month, int year);

        public Task<DayAccounting> CheckDayForExistanceAsync(DateTime date, Guid userId);
    }
}
