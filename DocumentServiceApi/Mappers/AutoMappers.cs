using AutoMapper;
using DocumentServiceApi.Models.Dto;
using DocumentServiceApi.Models.Entities;

namespace DocumentServiceApi.Mappers
{
    public class AutoMappers : Profile
    {
        public AutoMappers() 
        {
            Documents();
        }

        private void Documents()
        {
            CreateMap<DocumentEntity, DocumentInfo>();
        }
    }
}
