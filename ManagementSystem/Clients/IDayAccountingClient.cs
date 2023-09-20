using Refit;
using TimeTrackingService.Models.Entities;

namespace UserService.Clients
{
    public interface IDayAccountingClient
    {
        [Get("/api/DayAccounting/getUsersDays")]
        public Task<List<DayAccounting>> GetDays(Guid id);

        [Get("/api/DayAccounting/workDaysCount")]
        public Task<int> GetWorkDaysCount(Guid id, int month, int year);

        [Get("/api/DayAccounting/sickDaysCount")]
        public Task<int> GetSickDaysCount(Guid id, int month, int year);

        [Get("/api/DayAccounting/holidaysCount")]
        public Task<int> GetHolidaysCount(Guid id, int month, int year);

        [Get("/api/DayAccounting/paidDaysCount")]
        public Task<int> GetPaidDaysCount(Guid id, int month, int year);

        [Get("/api/DayAccounting/getUsersDays/{id}")]
        public Task<List<DayAccounting>> GetUsersDays(Guid id);
    }
}
