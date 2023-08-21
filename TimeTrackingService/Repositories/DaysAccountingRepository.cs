using Microsoft.EntityFrameworkCore;
using TimeTrackingService.Data;
using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Models.Entities;
using TimeTrackingService.Models.Enums;

namespace TimeTrackingService.Repositories
{
    public class DaysAccountingRepository : IDaysAccountingRepository
    {
        private readonly TimeTrackingContext timeTrackingContext;
        public DaysAccountingRepository(TimeTrackingContext _timeTrackingContext)
        {
            timeTrackingContext = _timeTrackingContext;
        }

        public async Task AddDay(DaysAccounting daysAccounting)
        {
            timeTrackingContext.Add(daysAccounting);
            await timeTrackingContext.SaveChangesAsync();
        }

        public async Task AddRangeOfDays(List<DaysAccounting> daysAccounts)
        {
            await timeTrackingContext.AddRangeAsync(daysAccounts);
            await timeTrackingContext.SaveChangesAsync();
        }

        public async Task<List<DaysAccounting>> GetUsersDays(Guid id)
        {
            //AsNoTracking??
            return await timeTrackingContext.DaysAccounting.Where(x => x.UserId == id).ToListAsync();
        }

        public async Task RemoveDay(Guid id)
        {
            timeTrackingContext.Remove(id);
            await timeTrackingContext.SaveChangesAsync();
        }

        public async Task RemoveRangeOfDays(List<Guid> ids)
        {
            timeTrackingContext.RemoveRange(ids);
            await timeTrackingContext.SaveChangesAsync();
        }

        public async Task UpdateDay(DaysAccounting daysAccounting)
        {
            timeTrackingContext.DaysAccounting.Update(daysAccounting);
            await timeTrackingContext.SaveChangesAsync();
        }

        public async Task ApproveDay(Guid id)
        {
            var day = await timeTrackingContext.DaysAccounting.FindAsync(id);
            if (day == null)
            {
                throw new Exception("There is no such day in DB");
            }
            day.IsConfirmed = true;
            await timeTrackingContext.SaveChangesAsync();
        }

        public int GetUsersWorkDaysCount(Guid id, int month)
        {
            var days = timeTrackingContext.DaysAccounting.Where(x => x.UserId == id && x.Month == month && x.AccountingType == AccountingTypes.Work);
            return days.Count();
        }

        public int GetUsersSickDaysCount(Guid id, int month)
        {
            var days = timeTrackingContext.DaysAccounting.Where(x => x.UserId == id && x.Month == month && x.AccountingType == AccountingTypes.Sick);
            return days.Count();
        }

        public int GetUsersHolidaysCount(Guid id, int month)
        {
            var days = timeTrackingContext.DaysAccounting.Where(x => x.UserId == id && x.Month == month && x.AccountingType == AccountingTypes.Holiday);
            return days.Count();
        }

        public int GetPaidDaysCount(Guid id, int month)
        {
            return GetUsersWorkDaysCount(id, month) 
                    + GetUsersSickDaysCount(id, month) 
                    + GetUsersHolidaysCount(id, month);
        }
    }
}
