using ManagementSystem.Data;
using ManagementSystem.Exceptions;
using ManagementSystem.Helpers;
using ManagementSystem.Interfaces.Repositories;
using ManagementSystem.Interfaces.Services;
using ManagementSystem.Models.Entities;
using ManagementSystem.Models.UserDTO;
using ManagementSystem.Models.UserModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace ManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration config;
        private readonly IUserRepository userRepository;

        public UserService( IConfiguration _config,
                          IUserRepository _userRepository)
        {
            config = _config;
            userRepository = _userRepository;
        }

        public async Task Create(SignUpModel signUpModel)
        {
            await userRepository.CreateUserAsync(signUpModel);
        }

        public async Task<List<UserInfoModel>> GetUsers()
        {
            return await userRepository.GetUsersAsync();
        }

        public string Login(SignInModel signInModel)
        {
            var user = userRepository.GetUserByEmailAsync(signInModel.Email).Result;
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
            user.Token = token;
            return token;
        }

        public void ChangePassword(Guid id, string oldPassword, string newPassword)
        {
            var currentUser = userRepository.GetUserByIdAsync(id).Result;
            if (currentUser == null)
            {
                throw new NotFoundException("Null User");
            }
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, currentUser.Password))
            {
                throw new InternalException("Wrong password!!!");
            }
            currentUser.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            userRepository.UpdateUserAsync(currentUser);
        }

        public string GetUserIdByToken(JwtSecurityToken token)
        {
            var userId = token.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return userId;
        }

        //отдельный вервис
        //public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using var hmac = new HMACSHA512();
        //    passwordSalt = hmac.Key;
        //    passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //}


        //отдельный вервис
        //public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        //{
        //    using var hmac = new HMACSHA512(passwordSalt);
        //    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //    return computedHash.SequenceEqual(passwordHash);
        //}

        //private string GenerateJwt(UserModel userModel)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.Email, userModel.Email),
        //        new Claim(ClaimTypes.NameIdentifier, userModel.Id.ToString())
        //    };

        //    var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:audience"],
        //                claims,
        //                expires: DateTime.Now.AddMinutes(15),
        //                signingCredentials: credentials);
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}
