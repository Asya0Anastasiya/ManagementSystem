using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Interfaces.Services
{
    public interface IDayAccountingService
    {
        public Task AddDay(DayAccountingModel daysAccountingModel, Guid id);

        public Task AddRangeOfDays(List<DayAccountingModel> days, Guid id);

        public Task<List<DayAccounting>> GetUsersDays(Guid id);

        public Task RemoveDay(Guid id);

        public Task RemoveRangeOfDays(List<Guid> ids);

        public Task UpdateDay(DayAccounting day);

        public Task ApproveDay(Guid id);

        public int GetUsersWorkDaysCount(Guid id, int month, int year);

        public int GetUsersSickDaysCount(Guid id, int month, int year);

        public int GetUsersHolidaysCount(Guid id, int month, int year);

        public int GetPaidDaysCount(Guid id, int month, int year);
    }
}
