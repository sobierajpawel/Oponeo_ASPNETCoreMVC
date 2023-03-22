using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Oponeo.CustomerManagement.ControllerBasedWebAPI.Infrastructure
{
    public class TokenGeneratorService
    {
        private readonly IConfiguration _configuration;
        public TokenGeneratorService(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        public string GetToken(string email)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var descriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(1),
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, email.Split("@",StringSplitOptions.None)[0]),
                    new Claim(ClaimTypes.Email, email),
                })
            };

            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
