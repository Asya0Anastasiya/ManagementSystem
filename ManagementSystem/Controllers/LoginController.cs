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
        private readonly IUser iUser;

        public LoginController(IConfiguration configuration,
                               Context _context,
                               IUser _iUser)
        {
            _configuration = configuration; 
            context = _context;
            iUser = _iUser;
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public IActionResult Login([FromBody] SignUpEntity userLogin)
        //{
        //    var user = Authenticate(userLogin);

        //    if (user != null) 
        //    {
        //        var token = Generate(user);
        //        return Ok(token);
        //    }
        //    return NotFound("User not found");
        //}

        [Route("loginWithIdentity")]
        [HttpPost]
        public async Task<IActionResult> LoginWithIdentity(SignInEntity signInEntity)
        {
            if (ModelState.IsValid)
            {
                var result = await iUser.PasswordSignInAsync(signInEntity);
                if (result.Succeeded)
                {
                    var token = GenerateJWTForIentity(signInEntity);
                    return Ok(token);
                }
            }
            return BadRequest("Invalid credentials"); 
        }

        // отдельный сервис для JWT???
        //private string Generate(UserEntity user)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.UserName),
        //        new Claim(ClaimTypes.Email, user.Email),
        //        new Claim(ClaimTypes.Surname, user.Surname),
        //        new Claim(ClaimTypes.Role, user.Role),
        //    };

        //    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:audience"],
        //                claims,
        //                expires: DateTime.Now.AddMinutes(15),
        //                signingCredentials: credentials);
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        private string GenerateJWTForIentity(SignInEntity user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(15),
                        signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //сделать что-то с ретурнами
        //private UserEntity Authenticate(SignUpEntity userLogin)
        //{
        //    UserEntity user = context.AppUsers.Where(x => x.Email == userLogin.Email).FirstOrDefault();
        //    if (user != null) 
        //    { 
        //        if (!iPassword.VerifyPasswordHash(userLogin.Password, user.PasswordHash, user.PasswordSalt))
        //        {
        //            return null;
        //        }
        //        return user;
        //    }
        //    return null;
        //}
    }
}
