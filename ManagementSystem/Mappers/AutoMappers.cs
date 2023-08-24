using AutoMapper;
using UserServiceAPI.Models.Entities;
using UserServiceAPI.Models.UserDto;

namespace UserServiceAPI.Mappers
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

            CreateMap<SignUpModel, UserEntity>();

            CreateMap<UserInfoModel, UserEntity>();
        } 
    }
}
