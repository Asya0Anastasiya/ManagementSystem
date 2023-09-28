using TimeTrackingService.Helpers.Filtering;
using TimeTrackingService.Helpers.Pagination;
using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Interfaces.Services
{
    public interface IDayAccountingService
    {
        public Task AddDay(CreateDayModel dayModel);

        public Task AddRangeOfDays(List<CreateDayModel> days);

        public Task<List<DayAccountingModel>> GetUsersDays(FilteringParameters parameters, PaginationParameters pagination);

        public Task<int> GetUnconfirmedDaysCount(Guid id);

        public Task RemoveDayAsync(Guid id);

        public Task RemoveRangeOfDays(List<Guid> ids);

        public Task ApproveDayAsync(Guid id);

        //public int GetUsersWorkDaysCount(Guid id, int month, int year);

        //public int GetUsersSickDaysCount(Guid id, int month, int year);

        //public int GetUsersHolidaysCount(Guid id, int month, int year);

        //public int GetPaidDaysCount(Guid id, int month, int year);

        public Task<UsersDaysModel> GetUsersDaysInfo(Guid id, int month, int year);
    }
}
