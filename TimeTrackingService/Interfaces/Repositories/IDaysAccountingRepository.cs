using Microsoft.AspNetCore.Mvc;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Interfaces.Repositories
{
    public interface IDaysAccountingRepository
    {
        public Task AddDay(DaysAccounting daysAccounting);

        public Task AddRangeOfDays(List<DaysAccounting> daysAccounts);

        public Task<List<DaysAccounting>> GetUsersDays(Guid id);

        public Task RemoveDay(Guid id);

        public Task RemoveRangeOfDays(List<Guid> ids);
    }
}
