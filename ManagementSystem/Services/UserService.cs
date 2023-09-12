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
        private readonly IImageRepository _imageRepository;
        private readonly IDaysAccountingClientRepository _client;
        private readonly IMapper _mapper;

        public UserService( IConfiguration config,
                          IUserRepository userRepository,
                          IMapper mapper,
                          IDaysAccountingClientRepository client,
                          IImageRepository imageRepository)
        {
            _config = config;
            _userRepository = userRepository;
            _mapper = mapper;
            _client = client;
            _imageRepository = imageRepository;
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

        public async Task<List<UserInfoModel>> GetUsersAsync(FilteringParameters parameters,
                                                            PaginationParameters pagination)
        {
            var users = await _userRepository.GetUsersAsync(parameters, pagination);
            return _mapper.Map<List<UserInfoModel>>(users);
        }

        public int GetUsersCount()
        {
            return _userRepository.GetUsersCount();
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

            var image = new Image
            {
                User = user,
                Data = memoryStream.ToArray()
            };

            await _imageRepository.SetUserImageAsync(image);
        }

        public async Task<byte[]> GetUserImageAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            
            if (user == null)
            {
                throw new NotFoundException("User Not FFFound");
            }


            var image = await _imageRepository.GetUserImageAsync(userId);

            if (image == null)
            {
                throw new NotFoundException("Image not found");
            }

            return image.Data;
        }
    }
}
