using AutoMapper;
using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Services
{
    public class DaysAccountingService : IDaysAccountingService
    {
        private readonly IDaysAccountingRepository repository;
        private readonly IMapper mapper;
        public DaysAccountingService(IDaysAccountingRepository _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }

        public async Task AddDay(DaysAccountingModel daysAccountingModel, Guid id)
        {
            var daysAccounting = mapper.Map<DaysAccounting>(daysAccountingModel);
            daysAccounting.IsConfirmed = false;
            daysAccounting.UserId = id;
            await repository.AddDay(daysAccounting);
        }

        public async Task AddRangeOfDays(List<DaysAccounting> days)
        {
            await repository.AddRangeOfDays(days);
        }

        public async Task<List<DaysAccounting>> GetUsersDays(Guid id)
        {
            return await repository.GetUsersDays(id);
        }

        public async Task RemoveDay(Guid id)
        {
            await repository.RemoveDay(id);
        }

        public async Task RemoveRangeOfDays(List<Guid> ids)
        {
            await repository.RemoveRangeOfDays(ids);
        }

        public async Task UpdateDay(DaysAccounting day)
        {
            await repository.UpdateDay(day);
        }

        public async Task ApproveDay(Guid id)
        {
            await repository.ApproveDay(id);
        }

        public int GetUsersWorkDaysCount(Guid id, int month)
        {
            return repository.GetUsersWorkDaysCount(id, month);
        }

        public int GetUsersSickDaysCount(Guid id, int month)
        {
            return repository.GetUsersSickDaysCount(id, month);
        }

        public int GetUsersHolidaysCount(Guid id, int month)
        {
            return repository.GetUsersHolidaysCount(id, month);
        }

        public int GetPaidDaysCount(Guid id, int month)
        {
            return repository.GetPaidDaysCount(id, month);
        }
    }
}
