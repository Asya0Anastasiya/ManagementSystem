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

        public Task<DayAccounting> GetDayByIdAsync(Guid id);

        public Task RemoveDay(Guid id);

        public Task RemoveRangeOfDays(List<Guid> ids);

        public Task UpdateDay(DayAccounting daysAccounting);

        public int GetUsersWorkDaysCount(Guid id, int month, int year);

        public int GetUsersSickDaysCount(Guid id, int month, int year);

        public int GetUsersHolidaysCount(Guid id, int month, int year);

        public int GetPaidDaysCount(Guid id, int month, int year);

        public UsersDaysModel GetUsersDaysInfo(Guid id, int month, int year);
    }
}
