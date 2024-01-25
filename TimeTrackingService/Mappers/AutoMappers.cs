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

            CreateMap<CreateDayModel, DayAccounting>()
                .ForMember(x => x.Day, opt => opt.MapFrom(src => src.Date.Day))
                .ForMember(x => x.Month, opt => opt.MapFrom(src => src.Date.Month))
                .ForMember(x => x.Year, opt => opt.MapFrom(src => src.Date.Year));
        }

        private void Documents()
        {
            CreateMap<UpcomingDocumentModel, Document>();

            CreateMap<Document, DocumentInfoModel>();

            CreateMap<Document, DocumentWithSourceIdModel>();
        }
    }
}
