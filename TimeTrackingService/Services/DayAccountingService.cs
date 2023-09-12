using AutoMapper;
using TimeTrackingService.Helpers.Filtering;
using TimeTrackingService.Helpers.Pagination;
using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Migrations;
using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Services
{
    public class DayAccountingService : IDayAccountingService
    {
        private readonly IDayAccountingRepository _repository;
        private readonly IMapper _mapper;
        public DayAccountingService(IDayAccountingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddDay(CreateDayModel dayModel)
        {
            var daysAccounting = _mapper.Map<DayAccounting>(dayModel);
            daysAccounting.IsConfirmed = false;
            daysAccounting.Day = dayModel.Date.Day;
            daysAccounting.Month = dayModel.Date.Month;
            daysAccounting.Year = dayModel.Date.Year;
            await _repository.AddDay(daysAccounting);
        }

        public async Task AddRangeOfDays(List<CreateDayModel> daysModel)
        {
            var days = _mapper.Map<List<DayAccounting>>(daysModel);
            for (var i = 0; i < daysModel.Count; i++)
            {
                days[i].IsConfirmed = false;
                days[i].Day = daysModel[i].Date.Day;
                days[i].Month = daysModel[i].Date.Month;
                days[i].Year = daysModel[i].Date.Year;
            }
            await _repository.AddRangeOfDays(days);
        }

        public async Task<List<DayAccountingModel>> GetUsersDays(FilteringParameters parameters,
                                                            PaginationParameters pagination)
        {
            var days = await _repository.GetUsersDays(parameters, pagination);
            return _mapper.Map<List<DayAccountingModel>>(days);
        }

        public async Task RemoveDay(Guid id)
        {
            await _repository.RemoveDay(id);
        }

        public async Task RemoveRangeOfDays(List<Guid> ids)
        {
            await _repository.RemoveRangeOfDays(ids);
        }

        public async Task UpdateDay(DayAccounting day)
        {
            await _repository.UpdateDay(day);
        }

        public async Task ApproveDay(Guid id)
        {
            var day = await _repository.GetDayByIdAsync(id);
            if (day == null)
            {
                throw new Exception("There is no such day in DB");
            }
            day.IsConfirmed = true;
            await UpdateDay(day);
        }

        public int GetUsersWorkDaysCount(Guid id, int month, int year)
        {
            return _repository.GetUsersWorkDaysCount(id, month, year);
        }

        public int GetUsersSickDaysCount(Guid id, int month, int year)
        {
            return _repository.GetUsersSickDaysCount(id, month, year);
        }

        public int GetUsersHolidaysCount(Guid id, int month, int year)
        {
            return _repository.GetUsersHolidaysCount(id, month, year);
        }

        public int GetPaidDaysCount(Guid id, int month, int year)
        {
            return _repository.GetPaidDaysCount(id, month, year);
        }

        public UsersDaysModel GetUsersDaysInfo(Guid id, int month, int year)
        {
            return _repository.GetUsersDaysInfo(id, month, year);
        }
    }
}
