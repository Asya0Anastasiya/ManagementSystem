using Microsoft.EntityFrameworkCore;
using TimeTrackingService.Data;
using TimeTrackingService.Helpers.Filtering;
using TimeTrackingService.Helpers.Pagination;
using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Models.Entities;
using TimeTrackingService.Models.Enums;
using TimeTrackingService.Models.Params;

namespace TimeTrackingService.Repositories
{
    public class DayAccountingRepository : IDayAccountingRepository
    {
        private readonly TimeTrackingContext _timeTrackingContext;
        public DayAccountingRepository(TimeTrackingContext timeTrackingContext)
        {
            _timeTrackingContext = timeTrackingContext;
        }

        public async Task AddDay(DayAccounting daysAccounting)
        {
            _timeTrackingContext.DaysAccounting.Add(daysAccounting);
            await _timeTrackingContext.SaveChangesAsync();
        }

        public async Task AddRangeOfDays(List<DayAccounting> daysAccounts)
        {
            await _timeTrackingContext.DaysAccounting.AddRangeAsync(daysAccounts);
            await _timeTrackingContext.SaveChangesAsync();
        }

        public async Task<List<DayAccounting>> GetUsersDays(FilteringParameters filtering, PaginationParameters pagination)
        {
            var days = _timeTrackingContext.DaysAccounting.AsNoTracking().AsQueryable();
            days = FilteringHelper.FilterDays(filtering, days);
            return await days.ToListAsync();
        }

        public async Task<int> GetUnconfirmedDaysCount(Guid id)
        {
            return await _timeTrackingContext.DaysAccounting.AsNoTracking()
                .Where(x => x.UserId == id && x.IsConfirmed == false).CountAsync();
        }

        public async Task RemoveDayAsync(DayAccounting day)
        {
            _timeTrackingContext.DaysAccounting.Remove(day);
            await _timeTrackingContext.SaveChangesAsync();
        }

        public async Task RemoveRangeOfDays(List<Guid> ids)
        {
            _timeTrackingContext.RemoveRange(ids);
            await _timeTrackingContext.SaveChangesAsync();
        }

        public async Task ApproveDayAsync(DayAccounting dayAccounting)
        {
            _timeTrackingContext.DaysAccounting.Update(dayAccounting);
            await _timeTrackingContext.SaveChangesAsync();
        }

        public async Task<DayAccounting> GetDayByIdAsync(Guid id)
        {
            return await _timeTrackingContext.DaysAccounting.FindAsync(id);
        }

        public async Task<int> GetUsersWorkDaysCount(Guid id, int month, int year)
        {
            var daysCount = await _timeTrackingContext.DaysAccounting.AsNoTracking()
                .Where(x => x.UserId == id 
                && x.Month == month
                && x.Year == year
                && x.AccountingType == AccountingTypes.Work).CountAsync();
            return daysCount;
        }

        public async Task<int> GetUsersSickDaysCount(Guid id, int month, int year)
        {
            var daysCount = await _timeTrackingContext.DaysAccounting.AsNoTracking()
                .Where(x => x.UserId == id 
                && x.Month == month
                && x.Year == year
                && x.AccountingType == AccountingTypes.Sick).CountAsync();
            return daysCount;
        }

        public async Task<int> GetUsersHolidaysCount(Guid id, int month, int year)
        {
            var daysCount = await _timeTrackingContext.DaysAccounting.AsNoTracking()
                .Where(x => x.UserId == id 
                && x.Month == month
                && x.Year == year
                && x.AccountingType == AccountingTypes.Holiday).CountAsync();
            return daysCount;
        }

        public async Task<DayAccounting> CheckDayForExistenceAsync(DateTime date, Guid userId)
        { 
            return await _timeTrackingContext.DaysAccounting
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Date.Date == date.Date);
        }
    }
}
