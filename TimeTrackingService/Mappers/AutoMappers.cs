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
            Documents();
        }

        private void Days()
        {
            CreateMap<DayAccountingModel, DayAccounting>();

            CreateMap<DayAccounting, DayAccountingModel>();

            CreateMap<CreateDayModel, DayAccounting>();
        }

        private void Documents()
        {
            CreateMap<UpcomingDocumentModel, Document>();
        }
    }
}
