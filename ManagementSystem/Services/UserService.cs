﻿using AutoMapper;
using UserServiceAPI.Exceptions;
using UserServiceAPI.Helpers;
using UserServiceAPI.Interfaces.Repositories;
using UserServiceAPI.Interfaces.Services;
using UserServiceAPI.Models.Entities;
using UserServiceAPI.Models.Enums;
using UserServiceAPI.Models.UserDto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UserServiceAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration config;
        private readonly IUserRepository userRepository;
        private readonly IDaysAccountingClientRepository client;
        private readonly IMapper mapper;

        public UserService( IConfiguration _config,
                          IUserRepository _userRepository,
                          IMapper _mapper,
                          IDaysAccountingClientRepository _client)
        {
            config = _config;
            userRepository = _userRepository;
            mapper = _mapper;
            client = _client;
        }

        public async Task Create(SignUpModel signUpModel)
        {
            if (!(await userRepository.GetUserByEmailAsync(signUpModel.Email) == null))
            {
                throw new InternalException("Such user already exists");
            }
            List<string> passErrors = PasswordValidator.CheckPasswordStrength(signUpModel.Password);
            if (passErrors.Count != 0)
            {
                throw new InternalException("Your paswword is not strength enought");
            }
            var user = mapper.Map<UserEntity>(signUpModel);
            user.Role = Roles.User;
            user.Password = BCrypt.Net.BCrypt.HashPassword(signUpModel.Password);
            await userRepository.CreateUserAsync(user);
        }

        public async Task<List<UserInfoModel>> GetUsersAsync()
        {
            var users = await userRepository.GetUsersAsync();
            return mapper.Map<List<UserInfoModel>>(users);
        }

        public async Task<UserInfoModel> GetUserInfo(Guid id, int month)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var userInfo = mapper.Map<UserInfoModel>(user);
            userInfo.WorkDays = await client.GetWorkDaysCount(user.Id, month);
            userInfo.SickDays = await client.GetSickDaysCount(user.Id, month);
            userInfo.Holidays = await client.GetHolidaysCount(user.Id, month);
            userInfo.PaidDays = await client.GetPaidDaysCount(user.Id, month);
            return userInfo;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            await userRepository.DeleteUserAsync(user);
        }

        public async Task<string> Login(SignInModel signInModel)
        {
            var user = await userRepository.GetUserByEmailAsync(signInModel.Email);
            if (user == null)
            {
                throw new NotFoundException("Such user does not exist");
            }
            if (!BCrypt.Net.BCrypt.Verify(signInModel.Password, user.Password))
            {
                throw new InternalException("Wrong password");
            }
            // user.Role.ToString() Ok????
            var token = new JwtGenerator(config).CreateJwt(user.Role.ToString(), user.Email);
            return token;
        }

        public async Task ChangePassword(Guid id, string oldPassword, string newPassword)
        {
            var currentUser = await userRepository.GetUserByIdAsync(id);
            if (currentUser == null)
            {
                throw new NotFoundException("User not found");
            }
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, currentUser.Password))
            {
                throw new InternalException("Wrong password!!!");
            }
            currentUser.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await userRepository.UpdateUserAsync(currentUser);
        }

        public string GetUserIdByToken(JwtSecurityToken token)
        {
            var userId = token.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return userId;
        }

        public async Task UpdateUserAsync(UserInfoModel model)
        {
            var user = await userRepository.GetUserByIdAsync(model.Id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            user = mapper.Map<UserEntity>(model);
            await userRepository.UpdateUserAsync(user);
        }
    }
}
