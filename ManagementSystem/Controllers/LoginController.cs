using ManagementSystem.Data;
using ManagementSystem.Entities;
using ManagementSystem.Interfaces;
using ManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Context context;
        private readonly IPassword iPassword;

        public LoginController(IConfiguration configuration,
                               Context _context,
                               IPassword _pass)
        {
            _configuration = configuration; 
            context = _context;
            iPassword = _pass;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null) 
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }

        // отдельный сервис для JWT???
        private string Generate(UserEntity user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(15),
                        signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //сделать что-то с ретурнами
        private UserEntity Authenticate(UserLogin userLogin)
        {
            UserEntity user = context.Users.Where(x => x.UserName == userLogin.Username).FirstOrDefault();
            if (user != null) 
            { 
                if (!iPassword.VerifyPasswordHash(userLogin.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return null;
                }
                return user;
            }
            return null;
        }
    }
}
