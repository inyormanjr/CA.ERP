using AutoMapper;
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

        public AuthenticationController(IMapper mapper,  IConfiguration config, UserService userService)
        {
            _mapper = mapper;
            _config = config;
            _userService = userService;
        }

        public IUserRepository AthenticationRepository { get; }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponse>> Register (RegisterRequest dto)
        {

            var id = await _userService.AddUserAsync(dto.UserName, dto.Password, dto.BranchId);

            //change to proper dto 
            return Ok(new RegisterResponse() {  UserId = id});
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
            var userId = await _userService.AuthenticateUser(loginCredentials.Username, loginCredentials.Password, cancellationToken);
            if (userId == null) return Unauthorized();

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
             SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
             
            return Ok(new LoginResponse() { 
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}
