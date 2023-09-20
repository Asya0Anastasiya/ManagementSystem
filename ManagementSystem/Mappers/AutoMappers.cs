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
            CreateMap<UserEntity, UserInfoModel>();

            CreateMap<SignUpModel, UserEntity>();

            CreateMap<UserInfoModel, UserEntity>();

            CreateMap<UpdateUserModel, UserEntity>();
        } 
    }
}
