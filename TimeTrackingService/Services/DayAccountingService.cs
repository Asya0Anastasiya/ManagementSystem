using AutoMapper;
using TimeTrackingService.Helpers.Pagination;
using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Entities;
using TimeTrackingService.Exceptions;
using TimeTrackingService.Models.Params;

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
            var day = await _repository.CheckDayForExistenceAsync(dayModel.Date, dayModel.UserId);

            if (day != null)
            {
                throw new InternalException($"That day -- {dayModel.Date} -- already exist");
            }

            var daysAccounting = _mapper.Map<DayAccounting>(dayModel);

            await _repository.AddDay(daysAccounting);
        }

        public async Task AddRangeOfDays(List<CreateDayModel> daysModels)
        {
            foreach (var dayModel in daysModels)
            {
                var day = await _repository.CheckDayForExistenceAsync(dayModel.Date, dayModel.UserId);

                if (day != null)
                {
                    throw new InternalException($"One of days -- {dayModel.Date} -- already exist");
                }
            }

            var days = _mapper.Map<List<DayAccounting>>(daysModels);

            await _repository.AddRangeOfDays(days);
        }

        public async Task<List<DayAccountingModel>> GetUsersDays(FilteringParameters parameters, int pageNumber, int pageSize)
        {
            var pagination = new PaginationParameters(pageNumber, pageSize);

            var days = await _repository.GetUsersDays(parameters, pagination);

            return _mapper.Map<List<DayAccountingModel>>(days);
        }

        public async Task<int> GetUnconfirmedDaysCount(Guid id)
        {
            return await _repository.GetUnconfirmedDaysCount(id);
        }

        public async Task RemoveDayAsync(Guid id)
        {
            var day = await _repository.GetDayByIdAsync(id);

            if (day == null)
            {
                throw new NotFoundException("This day not found");
            }

            await _repository.RemoveDayAsync(day);
        }

        public async Task RemoveRangeOfDays(List<Guid> ids)
        {
            await _repository.RemoveRangeOfDays(ids);
        }

        public async Task ApproveDayAsync(Guid id)
        {
            var day = await _repository.GetDayByIdAsync(id);

            if (day == null)
            {
                throw new Exception("Day not found");
            }

            day.IsConfirmed = true;

            await _repository.ApproveDayAsync(day);
        }

        public async Task<DayAccounting> GetUserDay(Guid userId, DateTime date)
        {
            var day = await _repository.CheckDayForExistenceAsync(date, userId);

            if (day == null)
            {
                throw new NotFoundException("Day not found");
            }

            return day;
        }

        private async Task<int> GetPaidDaysCount(Guid id, int month, int year)
        {
            var daysCount = await _repository.GetUsersWorkDaysCount(id, month, year)
                    + await _repository.GetUsersSickDaysCount(id, month, year)
                    + await _repository.GetUsersHolidaysCount(id, month, year);
            return daysCount;
        }

        public async Task<UsersDaysModel> GetUsersDaysInfo(Guid id, int month, int year)
        {
            UsersDaysModel model = new()
            {
                WorkDaysCount = await _repository.GetUsersWorkDaysCount(id, month, year),
                SickDaysCount = await _repository.GetUsersSickDaysCount(id, month, year),
                HolidaysCount = await _repository.GetUsersHolidaysCount(id, month, year),
                PaidDaysCount = await GetPaidDaysCount(id, month, year)
            };
            return model;
        }
    }
}
