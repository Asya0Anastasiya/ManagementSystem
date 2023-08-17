using AutoMapper;
using ManagementSystem.Models.Entities;
using ManagementSystem.Models.Enums;
using ManagementSystem.Models.UserDTO;
using ManagementSystem.Models.UserModels;

namespace ManagementSystem.Mappers
{
    public class AutoMappers : Profile
    {
        public AutoMappers()
        {
            Users();
        }

        private void Users()
        {
            CreateMap<UserEntity, UserInfoModel>();

            CreateMap<SignUpModel, UserEntity>()
                .ForMember(dest =>
                    dest.Role,
                    opt => opt.MapFrom(src => Roles.Admin))
                .ForMember(dest =>
                    dest.Password,
                    opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));
        } 
    }
}
