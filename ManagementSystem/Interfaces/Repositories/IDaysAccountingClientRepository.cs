using TimeTrackingService.Models.Entities;

namespace UserServiceAPI.Interfaces.Repositories
{
    public interface IDaysAccountingClientRepository
    {
        public Task<List<DayAccounting>> GetDays(Guid id);

        public Task<int> GetWorkDaysCount(Guid id, int month);

        public Task<int> GetSickDaysCount(Guid id, int month);

        public Task<int> GetHolidaysCount(Guid id, int month);

        public Task<int> GetPaidDaysCount(Guid id, int month);
    }
}
