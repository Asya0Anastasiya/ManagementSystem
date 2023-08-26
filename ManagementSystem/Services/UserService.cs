using AutoMapper;
using UserServiceAPI.Exceptions;
using UserServiceAPI.Helpers;
using UserServiceAPI.Helpers.Filtering;
using UserServiceAPI.Helpers.Pagination;
using UserServiceAPI.Interfaces.Repositories;
using UserServiceAPI.Interfaces.Services;
using UserServiceAPI.Models.Entities;
using UserServiceAPI.Models.Enums;
using UserServiceAPI.Models.UserDto;

namespace UserServiceAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly IDaysAccountingClientRepository _client;
        private readonly IMapper _mapper;

        public UserService( IConfiguration config,
                          IUserRepository userRepository,
                          IMapper mapper,
                          IDaysAccountingClientRepository client)
        {
            _config = config;
            _userRepository = userRepository;
            _mapper = mapper;
            _client = client;
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

        public async Task<PagedList<UserInfoModel>> GetUsersAsync(FilteringParameters parameters,
                                                            PaginationParameters pagination)
        {
            var users = await _userRepository.GetUsersAsync(parameters, pagination);
            return _mapper.Map<PagedList<UserInfoModel>>(users);
        }

        public async Task<UserInfoModel> GetUserInfo(Guid id, int month)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var userInfo = _mapper.Map<UserInfoModel>(user);
            userInfo.WorkDays = await _client.GetWorkDaysCount(user.Id, month);
            userInfo.SickDays = await _client.GetSickDaysCount(user.Id, month);
            userInfo.Holidays = await _client.GetHolidaysCount(user.Id, month);
            userInfo.PaidDays = await _client.GetPaidDaysCount(user.Id, month);
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
            var token = new JwtGenerator(_config).CreateJwt(user.Role.ToString(), user.Email);
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
                throw new InternalException("Wrong password!!!");
            }
            currentUser.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdateUserAsync(currentUser);
        }

        public async Task UpdateUserAsync(UserInfoModel model)
        {
            var user = await _userRepository.GetUserByIdAsync(model.Id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            user = _mapper.Map<UserEntity>(model);
            await _userRepository.UpdateUserAsync(user);
        }
    }
}
