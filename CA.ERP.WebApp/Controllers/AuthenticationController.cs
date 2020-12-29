using AutoMapper;
using CA.ERP.Domain.Helpers;
using CA.ERP.Domain.UserAgg;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers
{
    public class AuthenticationController:BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly UserService _userService;
        private readonly EnumFlagsHelper _enumFlagsHelper;

        public AuthenticationController(IMapper mapper,  IConfiguration config, UserService userService, EnumFlagsHelper enumFlagsHelper)
        {
            _mapper = mapper;
            _config = config;
            _userService = userService;
            _enumFlagsHelper = enumFlagsHelper;
        }



        /// <summary>
        /// Login using username and password
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Token use to login</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("Login")]
        public async Task<ActionResult<Dto.LoginResponse>> Login(Dto.LoginRequest loginCredentials, CancellationToken cancellationToken)
        {
            var optionUserId = await _userService.AuthenticateUser(loginCredentials.Username, loginCredentials.Password, cancellationToken);

                var actionResult = optionUserId.Match<ActionResult>(f0: user =>
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

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config.GetSection("AppSettings:Token").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(30),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new Dto.LoginResponse()
                {
                    token = tokenHandler.WriteToken(token)
                });
            },
            f1: (none) => Unauthorized());

            return actionResult;
        }
    }
}
