using AutoMapper;
using UserService.Exceptions;
using UserService.Helpers;
using UserService.Helpers.Pagination;
using UserService.Interfaces.Repositories;
using UserService.Interfaces.Services;
using UserService.Models.Entities;
using UserService.Models.Enums;
using UserService.Models.TokenDto;
using UserService.Models.Params;
using UserService.Models.UserDto;
using Microsoft.Extensions.Options;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly RefreshTokenOptions _refreshTokenOptions;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IConfiguration config,
                          IUserRepository userRepository,
                          IMapper mapper,
                          IOptions<RefreshTokenOptions> refreshTokenOptions)
        {
            _config = config;
            _userRepository = userRepository;
            _mapper = mapper;
            _refreshTokenOptions = refreshTokenOptions.Value;
        }

        private const int MaxFileSize = 10_485_760;

        public async Task Create(SignUpModel signUpModel)
        {
            var user = await _userRepository.GetUserByEmailAsync(signUpModel.Email);

            if (user != null)
            {
                throw new InternalException("Such user already exists");
            }

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

        public async Task<Tokens> Login(SignInModel signInModel)
        {
            var user = await _userRepository.GetUserByEmailAsync(signInModel.Email);

            if (user == null)
            {
                throw new NotFoundException("Invalid credentials");
            }

            if (!BCrypt.Net.BCrypt.Verify(signInModel.Password, user.Password))
            {
                throw new InternalException("Wrong password");
            }

            var token = new JwtGenerator(_config).CreateJwt(user.Role.ToString(), user.Email, user.Id, user.Position.Department.Name);

            var refreshToken = new RefreshToken
            {
                Token = new JwtGenerator(_config).GenerateRefreshToken(),
                UserId = user.Id,
                CreatedDateTime = DateTime.Now
            };

            if (user.RefreshToken != null)
            {
                await _userRepository.RemoveRefreshTokenAsync(user);
            }

            await _userRepository.AddRefreshTokenAsync(refreshToken);

            return new Tokens
            {
                Token = token,
                RefreshToken = refreshToken.Token
            };
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
            var user = await _userRepository.GetUserByIdAsync(model.Id);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            user.FirstName = model.FirstName.Trim(); 
            user.LastName = model.LastName.Trim();
            user.Email = model.Email.Trim();
            user.PhoneNumber = model.PhoneNumber.Trim();

            await _userRepository.UpdateUserAsync(user);
        }

        public async Task SetUserImageAsync(Guid userId, IFormFile file)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User Not Found");
            }

            if (file.Length > MaxFileSize)
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
                throw new NotFoundException("User Not found");
            }

            if (user.UserImage == null)
            {
                throw new NotFoundException("Image not found");
            }

            return user.UserImage;
        }

        public async Task<Tokens> ValidateRefreshTokenAsync(string refreshToken)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken);

            if (user == null)
            {
                throw new InternalException("Invalid refresh token.");
            }

            var refreshTokenExpires = new TimeSpan(_refreshTokenOptions.RefreshTokenExpiresDays, 
                                                    _refreshTokenOptions.RefreshTokenExpiresHours, 
                                                    _refreshTokenOptions.RefreshTokenExpiresMinutes, 
                                                    _refreshTokenOptions.RefreshTokenExpiresSeconds);

            if (DateTime.Now - user.RefreshToken.CreatedDateTime > refreshTokenExpires)
            {
                await _userRepository.RemoveRefreshTokenAsync(user);

                throw new InternalException("Invalid refresh token.");
            }

            var newRefreshToken = new RefreshToken
            {
                Token = new JwtGenerator(_config).GenerateRefreshToken(),
                User = user,
                CreatedDateTime = DateTime.Now
            };

            await _userRepository.AddRefreshTokenAsync(newRefreshToken);

            var newJwtToken = new JwtGenerator(_config).CreateJwt(user.Role.ToString(), user.Email, user.Id, user.Position.Department.Name);

            return new Tokens
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken.Token
            };
        }
    }
}
