using AutoMapper;
using UserService.Models.Entities;
using UserService.Models.UserDto;

namespace UserService.Mappers
{
    public class AutoMappers : Profile
    {
        public AutoMappers()
        {
            Users();
        }

        private void Users()
        {
            CreateMap<UserEntity, UserInfoModel>()
                .ForMember(x => x.Position, opt => opt.MapFrom(src => src.Position.Name))
                .ForMember(x => x.Department, opt => opt.MapFrom(src => src.Position.Department.Name))
                .ForMember(x => x.BranchOffice, opt => opt.MapFrom(src => src.Position.Department.BranchOffice.Name))
                .ForMember(x => x.Role, opt => opt.MapFrom(src => src.Role.ToString()));

            CreateMap<SignUpModel, UserEntity>();

            CreateMap<UserInfoModel, UserEntity>();

            CreateMap<UpdateUserModel, UserEntity>();
        } 
    }
}
