using AutoMapper;
using TimeTrackingService.Helpers.Filtering;
using TimeTrackingService.Helpers.Pagination;
using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Entities;
using TimeTrackingService.Exceptions;

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
            var day = await _repository.CheckDayForExistanceAsync(dayModel.Date, dayModel.UserId);

            if (day != null)
            {
                throw new InternalException($"That day -- {dayModel.Date} -- already exist");
            }
            var daysAccounting = _mapper.Map<DayAccounting>(dayModel);
            //daysAccounting.IsConfirmed = false;
            daysAccounting.Day = dayModel.Date.Day;
            daysAccounting.Month = dayModel.Date.Month;
            daysAccounting.Year = dayModel.Date.Year;
            await _repository.AddDay(daysAccounting);
        }

        public async Task AddRangeOfDays(List<CreateDayModel> daysModels)
        {
            foreach (var dayModel in daysModels)
            {
                var day = await _repository.CheckDayForExistanceAsync(dayModel.Date, dayModel.UserId);

                if (day != null)
                {
                    throw new InternalException($"One of days -- {dayModel.Date} -- already exist");
                }
            }

            var days = _mapper.Map<List<DayAccounting>>(daysModels);
            for (var i = 0; i < daysModels.Count; i++)
            {
                //days[i].IsConfirmed = false;
                days[i].Day = daysModels[i].Date.Day;
                days[i].Month = daysModels[i].Date.Month;
                days[i].Year = daysModels[i].Date.Year;
            }
            await _repository.AddRangeOfDays(days);
        }

        public async Task<List<DayAccountingModel>> GetUsersDays(FilteringParameters parameters,
                                                            PaginationParameters pagination)
        {
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
                throw new Exception("There is no such day in DB");
            }
            day.IsConfirmed = true;
            await _repository.ApproveDayAsync(day);
        }

        public async Task<DayAccounting> GetUserDay(Guid userId, DateTime date)
        {
            //if == null throw...
            return await _repository.CheckDayForExistanceAsync(date, userId);
        }

        public async Task<UsersDaysModel> GetUsersDaysInfo(Guid id, int month, int year)
        {
            return await _repository.GetUsersDaysInfo(id, month, year);
        }
    }
}
