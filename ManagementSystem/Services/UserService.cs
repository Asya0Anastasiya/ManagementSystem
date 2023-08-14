using ManagementSystem.Data;
using ManagementSystem.Helpers;
using ManagementSystem.Interfaces;
using ManagementSystem.Models;
using ManagementSystem.Models.UserDTO;
using ManagementSystem.Models.UserModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

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
            if (!CheckUserExist(signUpModel.Email))
            {
                string passErrors = CheckPasswordStrength(signUpModel.Password);
                if (string.IsNullOrEmpty(passErrors))
                {
                    UserModel userModel = new();
                    CreatePasswordHash(signUpModel.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    userModel.Email = signUpModel.Email;
                    userModel.FirstName = signUpModel.Firstname;
                    userModel.LastName = signUpModel.Lastname;
                    userModel.Role = "user";
                    userModel.PasswordHash = passwordHash;
                    userModel.PasswordSalt = passwordSalt;
                    context.Users.Add(userModel);
                    context.SaveChanges();
                }
                else throw new Exception(passErrors);
            }
            else throw new Exception("Such user already exists");
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
                var token = new JwtGenerator(config).CreateJwt(user);
                user.Token = token;
                return token;
                //var token = GenerateJwt(user);
                //return token;
            }
            return string.Empty;
            //return false;
        }

        private UserModel Authenticate(SignInModel signInModel)
        {
            var user = context.Users.Where(x => x.Email == signInModel.Email).FirstOrDefault();
            if (user != null)
            {
                if (!VerifyPasswordHash(signInModel.Password, user.PasswordHash, user.PasswordSalt))
                {
                    throw new Exception("Wrong password");
                }
                return user;
            }
            else throw new Exception("Such user does not exist");
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

        //отдельный вервис
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }


        //отдельный вервис
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        //отдельный вервис
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

        private bool CheckUserExist(string email)
        => context.Users.Any(x => x.Email == email);

        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new();
            if (password.Length < 8)
                sb.Append("Password length should be more than 8" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password should be Alphanumeric" + Environment.NewLine);
            if (!Regex.IsMatch(password, "[!,@,#,$,%,^,&,*,(,),_,=,+,{,}]"))
                sb.Append("Password should contain special chars" + Environment.NewLine);
            return sb.ToString();
        }
    }
}
