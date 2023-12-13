using AuthApi.Dtos;
using AuthApi.Dtos.Enteties;
using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<User> _userManager;

        public TokenService(IConfiguration config, UserManager<User> userManager)
        {
            _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["Jwt:Key"]));
            _userManager = userManager;
        }
        public async Task<string> CreateToken(User user)
        {
            try
            {
                
                var keyBytes = new byte[64]; // 64 bytes = 512 bits
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(keyBytes);
                }

                var roles = await _userManager.GetRolesAsync(user);
                var tokenHandler = new JwtSecurityTokenHandler();
                var symmetricKey = new SymmetricSecurityKey(keyBytes);
                var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                      new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
                      new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                      new Claim(ClaimTypes.Name, user.UserName),
                    }.Concat(roles.Select(role => new Claim(ClaimTypes.Role, role)))),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = signingCredentials
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }


        }
    }
}
