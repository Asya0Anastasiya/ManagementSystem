using ManagementSystem.Data;
using ManagementSystem.Interfaces;
using ManagementSystem.Models;
using ManagementSystem.Models.UserDTO;
using ManagementSystem.Models.UserModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly Context context;
        private readonly IConfiguration config;

        public UserService(Context _context, IConfiguration _config)
        {
            context = _context;
            config = _config;
        }

        // вынести в отдельный метод создание UserModel из signUpModel???
        public void Create(SignUpModel signUpModel)
        {
            UserModel userModel = new();
            CreatePasswordHash(signUpModel.Password, out byte[] passwordHash, out byte[] passwordSalt);
            userModel.Email = signUpModel.Email;
            userModel.FirstName = signUpModel.Firstname;
            userModel.LastName = signUpModel.Lastname;
            userModel.PasswordHash = passwordHash;
            userModel.PasswordSalt = passwordSalt;
            context.Users.Add(userModel);
            context.SaveChanges();
        }

        public IQueryable<UserInfoModel> GetUsers()
        {
            return context.Users.Select(user => new UserInfoModel
            {
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
            }); 
        }

        public string Login(SignInModel signInModel)
        {
            var user = Authenticate(signInModel);
            if (user != null)
            {
                var token = GenerateJwt(user);
                return token;
            }
            return string.Empty;
        }

        //null
        private UserModel Authenticate(SignInModel signInModel)
        {
            var user = context.Users.Where(x => x.Email == signInModel.Email).FirstOrDefault();
            if (user != null)
            {
                if (!VerifyPasswordHash(signInModel.Password, user.PasswordHash, user.PasswordSalt))
                {
                    throw new Exception("Wrong password");
                    //return null;
                }
                return user;
            }
            else throw new Exception("Such user does not exist");
            return null;
        }

        public bool ChangePassword(string userId, string oldPassword, string newPassword)
        {
            //JwtSecurityToken jwtSecurityToken = new(token);
            //string userId = GetUserIdByToken(jwtSecurityToken);
            // async
            var currentUser = context.Users.Where(x => x.Id.ToString().Equals(userId)).First();
            if (!VerifyPasswordHash(oldPassword, currentUser.PasswordHash, currentUser.PasswordSalt))
            {
                return false;
            }
            else
            {
                CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
                currentUser.PasswordHash = passwordHash;
                currentUser.PasswordSalt = passwordSalt;
                context.Users.Update(currentUser);
                context.SaveChanges();
                return true;
            }
        }

        public string GetUserIdByToken(JwtSecurityToken token)
        {
            var userId = token.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return userId;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        private string GenerateJwt(UserModel userModel)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, userModel.Email),
                new Claim(ClaimTypes.NameIdentifier, userModel.Id.ToString())
            };

            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(15),
                        signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
