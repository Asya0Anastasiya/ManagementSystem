using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Interfaces.Services
{
    public interface IDaysAccountingService
    {
        public Task AddDay(DaysAccounting daysAccounting);

        public Task AddRangeOfDays(List<DaysAccounting> days);

        public Task<List<DaysAccounting>> GetUsersDays(Guid id);

        public Task RemoveDay(Guid id);

        public Task RemoveRangeOfDays(List<Guid> ids);
    }
}
