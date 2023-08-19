using Microsoft.EntityFrameworkCore;
using TimeTrackingService.Data;
using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Models.Entities;

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
    }
}
