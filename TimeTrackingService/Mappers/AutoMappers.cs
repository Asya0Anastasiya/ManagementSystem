using AutoMapper;
using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Mappers
{
    public class AutoMappers : Profile
    {
        public AutoMappers()
        {
            Days();
        }

        private void Days()
        {
            CreateMap<DaysAccountingModel, DaysAccounting>();
        }
    }
}
