using Microsoft.EntityFrameworkCore;
using TimeTrackingService.Data;
using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Models.Entities;
using TimeTrackingService.Models.Enums;

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
            _timeTrackingContext.Add(daysAccounting);
            await _timeTrackingContext.SaveChangesAsync();
        }

        public async Task AddRangeOfDays(List<DayAccounting> daysAccounts)
        {
            await _timeTrackingContext.AddRangeAsync(daysAccounts);
            await _timeTrackingContext.SaveChangesAsync();
        }

        public async Task<List<DayAccounting>> GetUsersDays(Guid id)
        {
            //AsNoTracking??
            return await _timeTrackingContext.DaysAccounting
                .Where(x => x.UserId == id).ToListAsync();
        }

        public async Task RemoveDay(Guid id)
        {
            _timeTrackingContext.Remove(id);
            await _timeTrackingContext.SaveChangesAsync();
        }

        public async Task RemoveRangeOfDays(List<Guid> ids)
        {
            _timeTrackingContext.RemoveRange(ids);
            await _timeTrackingContext.SaveChangesAsync();
        }

        public async Task UpdateDay(DayAccounting daysAccounting)
        {
            _timeTrackingContext.DaysAccounting.Update(daysAccounting);
            await _timeTrackingContext.SaveChangesAsync();
        }

        public async Task<DayAccounting> GetDayByIdAsync(Guid id)
        {
            return await _timeTrackingContext.DaysAccounting.FindAsync(id);
        }

        public int GetUsersWorkDaysCount(Guid id, int month, int year)
        {
            var days = _timeTrackingContext.DaysAccounting
                .Where(x => x.UserId == id 
                && x.Month == month
                && x.Year == year
                && x.AccountingType == AccountingTypes.Work);
            return days.Count();
        }

        public int GetUsersSickDaysCount(Guid id, int month, int year)
        {
            var days = _timeTrackingContext.DaysAccounting
                .Where(x => x.UserId == id 
                && x.Month == month
                && x.Year == year
                && x.AccountingType == AccountingTypes.Sick);
            return days.Count();
        }

        public int GetUsersHolidaysCount(Guid id, int month, int year)
        {
            var days = _timeTrackingContext.DaysAccounting
                .Where(x => x.UserId == id 
                && x.Month == month
                && x.Year == year
                && x.AccountingType == AccountingTypes.Holiday);
            return days.Count();
        }

        public int GetPaidDaysCount(Guid id, int month, int year)
        {
            return GetUsersWorkDaysCount(id, month, year) 
                    + GetUsersSickDaysCount(id, month, year) 
                    + GetUsersHolidaysCount(id, month, year);
        }
    }
}
