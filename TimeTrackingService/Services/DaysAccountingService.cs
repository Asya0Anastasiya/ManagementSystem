using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Services
{
    public class DaysAccountingService : IDaysAccountingService
    {
        private readonly IDaysAccountingRepository repository;
        public DaysAccountingService(IDaysAccountingRepository _repository)
        {
              repository = _repository;
        }

        public async Task AddDay(DaysAccounting daysAccounting)
        {
            await repository.AddDay(daysAccounting);
        }

        public async Task AddRangeOfDays(List<DaysAccounting> days)
        {
            await repository.AddRangeOfDays(days);
        }

        public async Task<List<DaysAccounting>> GetUsersDays(Guid id)
        {
            return await repository.GetUsersDays(id);
        }

        public async Task RemoveDay(Guid id)
        {
            await repository.RemoveDay(id);
        }

        public async Task RemoveRangeOfDays(List<Guid> ids)
        {
            await repository.RemoveRangeOfDays(ids);
        }
    }
}
