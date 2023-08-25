using Microsoft.AspNetCore.Mvc;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Interfaces.Repositories
{
    public interface IDayAccountingRepository
    {
        public Task AddDay(DayAccounting daysAccounting);

        public Task AddRangeOfDays(List<DayAccounting> daysAccounts);

        public Task<List<DayAccounting>> GetUsersDays(Guid id);

        public Task<DayAccounting> GetDayByIdAsync(Guid id);

        public Task RemoveDay(Guid id);

        public Task RemoveRangeOfDays(List<Guid> ids);

        public Task UpdateDay(DayAccounting daysAccounting);

        public int GetUsersWorkDaysCount(Guid id, int month, int year);

        public int GetUsersSickDaysCount(Guid id, int month, int year);

        public int GetUsersHolidaysCount(Guid id, int month, int year);

        public int GetPaidDaysCount(Guid id, int month, int year);
    }
}
