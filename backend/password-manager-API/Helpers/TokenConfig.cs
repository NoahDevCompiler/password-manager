using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using password_manager_API.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using password_manager_API.Models.Interfaces;

namespace password_manager_API.Helpers
{
    public sealed class TokenConfig(IConfiguration configuration)
    {
        public string Create(IUserNameProvider user) {

            string secretKey = configuration!["Jwt:Secret"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor {

                Subject = new ClaimsIdentity([
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username.ToString())
                    ]),
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };

            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDescriptor);
            return token;
        }

    }
}
