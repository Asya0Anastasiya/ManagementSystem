﻿using AutoMapper;
using UserService.Exceptions;
using UserService.Helpers;
using UserService.Helpers.Pagination;
using UserService.Interfaces.Repositories;
using UserService.Interfaces.Services;
using UserService.Models.Entities;
using UserService.Models.Enums;
using UserService.Models.UserDto;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService( IConfiguration config,
                          IUserRepository userRepository,
                          IMapper mapper)
        {
            _config = config;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task Create(SignUpModel signUpModel)
        {
            var user = await _userRepository.GetUserByEmailAsync(signUpModel.Email);
            if (user != null)
            {
                throw new InternalException("Such user already exists");
            }
            PasswordValidator.CheckPasswordStrength(signUpModel.Password);
            user = _mapper.Map<UserEntity>(signUpModel);
            user.Role = Roles.User;
            user.Password = BCrypt.Net.BCrypt.HashPassword(signUpModel.Password);
            await _userRepository.CreateUserAsync(user);
        }

        public async Task<List<UserInfoModel>> GetUsersAsync(FilteringParameters parameters, int pageNumber, int pageSize)
        {
            var pagination = new PaginationParameters(pageNumber, pageSize);
            var users = await _userRepository.GetUsersAsync(parameters, pagination);
            return _mapper.Map<List<UserInfoModel>>(users);
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await _userRepository.GetUsersCountAsync();
        }

        public async Task<UserInfoModel> GetUserInfo(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var userInfo = _mapper.Map<UserInfoModel>(user);
            return userInfo;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            await _userRepository.DeleteUserAsync(user);
        }

        public async Task<string> Login(SignInModel signInModel)
        {
            var user = await _userRepository.GetUserByEmailAsync(signInModel.Email);
            if (user == null)
            {
                throw new NotFoundException("Such user does not exist");
            }
            if (!BCrypt.Net.BCrypt.Verify(signInModel.Password, user.Password))
            {
                throw new InternalException("Wrong password");
            }
            var token = new JwtGenerator(_config).CreateJwt(user.Role.ToString(), user.Email, user.Id);
            return token;
        }

        public async Task ChangePassword(Guid id, string oldPassword, string newPassword)
        {
            var currentUser = await _userRepository.GetUserByIdAsync(id);
            if (currentUser == null)
            {
                throw new NotFoundException("User not found");
            }
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, currentUser.Password))
            {
                throw new InternalException("Wrong password");
            }
            currentUser.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdateUserAsync(currentUser);
        }

        public async Task UpdateUserAsync(UpdateUserModel model)
        {
            // добавила AsNoTracking, иначе выскакивала ошибка, что EF не может
            // обновить сущность, так как две сущности с одинаковыми айди трекаются
            var user = await _userRepository.GetUserByIdAsync(model.Id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            user.FirstName = model.FirstName.Trim(); 
            user.LastName = model.LastName.Trim();
            user.Email = model.Email.Trim();
            user.PhoneNumber = model.PhoneNumber.Trim();
            //user = _mapper.Map<UserEntity>(model);
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task SetUserImageAsync(Guid userId, IFormFile file)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User Not Found");
            }

            if (file.Length > 5242880 * 2)
            {
                throw new InternalException("File is more then 10mb.");
            }

            if (file.Length == 0)
            {
                throw new InternalException("File length is 0.");
            }

            using var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);
            user.UserImage = memoryStream.ToArray();
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task<byte[]> GetUserImageAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User Not FFFound");
            }

            if (user.UserImage == null)
            {
                throw new NotFoundException("Image not found");
            }

            return user.UserImage;
        }
    }
}
