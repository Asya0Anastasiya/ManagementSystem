using AutoMapper;
using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Interfaces.Services;
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

        public async Task AddDay(DayAccountingModel daysAccountingModel, Guid id)
        {
            var daysAccounting = _mapper.Map<DayAccounting>(daysAccountingModel);
            daysAccounting.IsConfirmed = false;
            daysAccounting.UserId = id;
            await _repository.AddDay(daysAccounting);
        }

        public async Task AddRangeOfDays(List<DayAccountingModel> daysModel, Guid id)
        {
            // Id!!!!!!!!!!!!!!!!!!!!!!
            var days = _mapper.Map<List<DayAccounting>>(daysModel);
            await _repository.AddRangeOfDays(days);
        }

        public async Task<List<DayAccounting>> GetUsersDays(Guid id)
        {
            return await _repository.GetUsersDays(id);
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
    }
}
