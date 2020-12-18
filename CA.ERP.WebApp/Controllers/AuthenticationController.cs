using AutoMapper;
using CA.ERP.Domain.UserAgg;
using CA.ERP.WebApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers
{
    public class AuthenticationController:BaseApiController
    {
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        public AuthenticationController(IMapper mapper, IAuthenticationRepository authenticationRepository, IConfiguration config)
        {
            this.mapper = mapper;
            AthenticationRepository = authenticationRepository;
            this.config = config;
        }

        public IAuthenticationRepository AthenticationRepository { get; }

        [HttpPost("Register")]
        public async Task<IActionResult> Register (UserRegistrationDTO dto)
        {
            var mapped = this.mapper.Map<User>(dto);
            var user = await this.AthenticationRepository.Register(mapped, dto.Password);
            //security issue don't return the user. It has password hash.
            return Ok(user);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginCredentials)
        {
            var user = await this.AthenticationRepository.Login(loginCredentials.Username, loginCredentials.Password);
            if (user == null) return Unauthorized();

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
             SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
             
            return Ok(new { 
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}
