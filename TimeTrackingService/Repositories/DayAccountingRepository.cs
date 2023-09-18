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
            var days = _timeTrackingContext.DaysAccounting.AsQueryable();
            FilteringHelper filteringHelper = new();
            days = filteringHelper.FilterDays(filtering, days);
            return await days.ToListAsync();
            //AsNoTracking??
            //return PagedList<DayAccounting>.ToPagedItems(days, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<int> GetUnconfirmedDaysCount(Guid id)
        {
            return await _timeTrackingContext.DaysAccounting.Where(x => x.UserId == id && x.IsConfirmed == false).CountAsync();
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

        public async Task ApproveDayAsync(DayAccounting dayAccounting)
        {
            _timeTrackingContext.DaysAccounting.Update(dayAccounting);
            await _timeTrackingContext.SaveChangesAsync();
        }

        public async Task<DayAccounting> GetDayByIdAsync(Guid id)
        {
            return await _timeTrackingContext.DaysAccounting.FindAsync(id);
        }

        public async Task<int> GetUsersWorkDaysCount(Guid id, int month, int year)
        {
            var daysCount = await _timeTrackingContext.DaysAccounting
                .Where(x => x.UserId == id 
                && x.Month == month
                && x.Year == year
                && x.AccountingType == AccountingTypes.Work).CountAsync();
            return daysCount;
        }

        public async Task<int> GetUsersSickDaysCount(Guid id, int month, int year)
        {
            var daysCount = await _timeTrackingContext.DaysAccounting
                .Where(x => x.UserId == id 
                && x.Month == month
                && x.Year == year
                && x.AccountingType == AccountingTypes.Sick).CountAsync();
            return daysCount;
        }

        public async Task<int> GetUsersHolidaysCount(Guid id, int month, int year)
        {
            var daysCount = await _timeTrackingContext.DaysAccounting
                .Where(x => x.UserId == id 
                && x.Month == month
                && x.Year == year
                && x.AccountingType == AccountingTypes.Holiday).CountAsync();
            return daysCount;
        }

        public async Task<int> GetPaidDaysCount(Guid id, int month, int year)
        {
            var daysCount = await GetUsersWorkDaysCount(id, month, year) 
                    + await GetUsersSickDaysCount(id, month, year) 
                    + await GetUsersHolidaysCount(id, month, year);
            return daysCount;
        }

        public async Task<UsersDaysModel> GetUsersDaysInfo(Guid id, int month, int year)
        {
            UsersDaysModel model = new UsersDaysModel
            {
                WorkDaysCount = await GetUsersWorkDaysCount(id, month, year),
                SickDaysCount = await GetUsersSickDaysCount(id, month, year),
                HolidaysCount = await GetUsersHolidaysCount(id, month, year),
                PaidDaysCount = await GetPaidDaysCount(id, month, year)
            };
            return model;
        }
    }
}
