using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FamilyApplication.CommonServices
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(int userId,string username,string Role)
        {
            var jwtsetting = _configuration.GetSection("Jwt");
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsetting["key"]));
            var cred = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())   
            };

            var token = new JwtSecurityToken(
                issuer: jwtsetting["Issuer"],
                audience: jwtsetting["Audience"],
                claims:claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtsetting["ExpiryMinutes"])),
                signingCredentials:cred
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
