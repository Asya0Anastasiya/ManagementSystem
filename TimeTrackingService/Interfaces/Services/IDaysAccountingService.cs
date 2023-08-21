using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Interfaces.Services
{
    public interface IDaysAccountingService
    {
        public Task AddDay(DaysAccountingModel daysAccountingModel, Guid id);

        public Task AddRangeOfDays(List<DaysAccounting> days);

        public Task<List<DaysAccounting>> GetUsersDays(Guid id);

        public Task RemoveDay(Guid id);

        public Task RemoveRangeOfDays(List<Guid> ids);

        public Task UpdateDay(DaysAccounting day);

        public Task ApproveDay(Guid id);

        public int GetUsersWorkDaysCount(Guid id, int month);

        public int GetUsersSickDaysCount(Guid id, int month);

        public int GetUsersHolidaysCount(Guid id, int month);

        public int GetPaidDaysCount(Guid id, int month);
    }
}
