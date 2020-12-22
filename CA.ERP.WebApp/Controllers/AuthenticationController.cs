using AutoMapper;
using CA.ERP.Domain.Helpers;
using CA.ERP.Domain.UserAgg;
using CA.ERP.WebApp.Dto;
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

        public IUserRepository AthenticationRepository { get; }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request, CancellationToken cancellationToken)
        {

            var result = await _userService.CreateUserAsync(request.UserName, request.Password, request.Branches, cancellationToken: cancellationToken);

            //change to proper dto 
            return result.Match<ActionResult>(
                f0: userId => Ok(new RegisterResponse() { UserId = userId }),
                f1: validationFailures => BadRequest(new ErrorResponse() { GeneralError = "Validation Error", ValidationErrors =_mapper.Map<List<ValidationError>>(validationFailures) }),
                f2 : error => BadRequest(new ErrorResponse() { GeneralError = error.Value})
                );
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
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginCredentials, CancellationToken cancellationToken)
        {
            var optionUserId = await _userService.AuthenticateUser(loginCredentials.Username, loginCredentials.Password, cancellationToken);

                var actionResult = optionUserId.Match<ActionResult>(f0: user =>
                {
                    
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
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

                return Ok(new LoginResponse()
                {
                    token = tokenHandler.WriteToken(token)
                });
            },
            f1: (none) => Unauthorized());

            return actionResult;
        }
    }
}
