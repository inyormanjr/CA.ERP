using CA.ERP.Domain.Base;
using CA.ERP.Domain.Helpers;
using CA.ERP.Domain.UserAgg;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.CustomAuthentication
{
    public class TokenGenerator : IHelper
    {
        private readonly EnumFlagsHelper _enumFlagsHelper;
        private readonly IConfiguration _configuration;

        public TokenGenerator(EnumFlagsHelper enumFlagsHelper, IConfiguration configuration)
        {
            _enumFlagsHelper = enumFlagsHelper;
            _configuration = configuration;
        }
        public string GenerateToken(User user)
        {
            var claims = new List<Claim> {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim("RoleInt", ((int)user.Role).ToString()),
                        new Claim("Username", user.Username),
                        new Claim("FirstName", user.FirstName),
                        new Claim("LastName", user.LastName),
                    };

            var roles = _enumFlagsHelper.ConvertToList(user.Role);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenTtl = _configuration.GetSection("AppSettings:TokenTtl")?.Get<int>() ?? 3600;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(tokenTtl),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
