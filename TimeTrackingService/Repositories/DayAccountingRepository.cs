using Microsoft.EntityFrameworkCore;
using TimeTrackingService.Data;
using TimeTrackingService.Helpers.Filtering;
using TimeTrackingService.Helpers.Pagination;
using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Entities;
using TimeTrackingService.Models.Enums;

namespace TimeTrackingService.Repositories
{
    public class DayAccountingRepository : IDayAccountingRepository
    {
        private readonly TimeTrackingContext _timeTrackingContext;
        public DayAccountingRepository(TimeTrackingContext timeTrackingContext)
        {
            _timeTrackingContext = timeTrackingContext;
        }

        public async Task AddDay(DayAccounting daysAccounting)
        {
            _timeTrackingContext.Add(daysAccounting);
            await _timeTrackingContext.SaveChangesAsync();
        }

        public async Task AddRangeOfDays(List<DayAccounting> daysAccounts)
        {
            await _timeTrackingContext.AddRangeAsync(daysAccounts);
            await _timeTrackingContext.SaveChangesAsync();
        }

        public async Task<List<DayAccounting>> GetUsersDays(FilteringParameters filtering, PaginationParameters pagination)
        {
            var days = await _timeTrackingContext.DaysAccounting.ToListAsync();
            FilteringHelper filteringHelper = new();
            days = filteringHelper.FilterDays(filtering, days);
            return days;
            //AsNoTracking??
            //return PagedList<DayAccounting>.ToPagedItems(days, pagination.PageNumber, pagination.PageSize);
        }

        public async Task RemoveDay(Guid id)
        {
            _timeTrackingContext.Remove(id);
            await _timeTrackingContext.SaveChangesAsync();
        }

        public async Task RemoveRangeOfDays(List<Guid> ids)
        {
            _timeTrackingContext.RemoveRange(ids);
            await _timeTrackingContext.SaveChangesAsync();
        }

        public async Task UpdateDay(DayAccounting daysAccounting)
        {
            _timeTrackingContext.DaysAccounting.Update(daysAccounting);
            await _timeTrackingContext.SaveChangesAsync();
        }

        public async Task<DayAccounting> GetDayByIdAsync(Guid id)
        {
            return await _timeTrackingContext.DaysAccounting.FindAsync(id);
        }

        public int GetUsersWorkDaysCount(Guid id, int month, int year)
        {
            var days = _timeTrackingContext.DaysAccounting
                .Where(x => x.UserId == id 
                && x.Month == month
                && x.Year == year
                && x.AccountingType == AccountingTypes.Work);
            return days.Count();
        }

        public int GetUsersSickDaysCount(Guid id, int month, int year)
        {
            var days = _timeTrackingContext.DaysAccounting
                .Where(x => x.UserId == id 
                && x.Month == month
                && x.Year == year
                && x.AccountingType == AccountingTypes.Sick);
            return days.Count();
        }

        public int GetUsersHolidaysCount(Guid id, int month, int year)
        {
            var days = _timeTrackingContext.DaysAccounting
                .Where(x => x.UserId == id 
                && x.Month == month
                && x.Year == year
                && x.AccountingType == AccountingTypes.Holiday);
            return days.Count();
        }

        public int GetPaidDaysCount(Guid id, int month, int year)
        {
            return GetUsersWorkDaysCount(id, month, year) 
                    + GetUsersSickDaysCount(id, month, year) 
                    + GetUsersHolidaysCount(id, month, year);
        }

        public UsersDaysModel GetUsersDaysInfo(Guid id, int month, int year)
        {
            UsersDaysModel model = new UsersDaysModel
            {
                WorkDaysCount = GetUsersWorkDaysCount(id, month, year),
                SickDaysCount = GetUsersSickDaysCount(id, month, year),
                HolidaysCount = GetUsersHolidaysCount(id, month, year),
                PaidDaysCount = GetPaidDaysCount(id, month, year)
            };
            return model;
        }
    }
}
