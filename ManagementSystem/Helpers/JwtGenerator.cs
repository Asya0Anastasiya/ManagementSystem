using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserService.Helpers
{
    public class JwtGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtGenerator(IConfiguration configuration)
        {
                _configuration = configuration;
        }
        public string CreateJwt(string role, string email, Guid id)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Secrets:secretKey"]));
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim("Role", role),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            });
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtHandler.CreateToken(tokenDescriptor);
            return jwtHandler.WriteToken(token);
        }
    }
}
