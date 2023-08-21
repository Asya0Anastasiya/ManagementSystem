using Refit;
using TimeTrackingService.Models.Entities;

namespace UserServiceAPI.Clients
{
    public interface IDayAccountingClient
    {
        [Get("/api/DaysAccounting/getUsersDays")]
        public Task<List<DaysAccounting>> GetDays(Guid id);

        [Get("/api/DaysAccounting/workDaysCount")]
        public Task<int> GetWorkDaysCount(Guid id, int month);

        [Get("/api/DaysAccounting/sickDaysCount")]
        public Task<int> GetSickDaysCount(Guid id, int month);

        [Get("/api/DaysAccounting/holidaysCount")]
        public Task<int> GetHolidaysCount(Guid id, int month);

        [Get("/api/DaysAccounting/paidDaysCount")]
        public Task<int> GetPaidDaysCount(Guid id, int month);
    }
}
