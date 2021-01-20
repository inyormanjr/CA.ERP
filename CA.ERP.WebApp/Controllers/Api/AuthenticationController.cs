using AutoMapper;
using CA.ERP.Domain.Helpers;
using CA.ERP.Domain.UserAgg;
using CA.ERP.WebApp.CustomAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers.Api
{
    public class AuthenticationController : BaseApiController
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly IConfiguration _config;
        private readonly UserService _userService;
        private readonly EnumFlagsHelper _enumFlagsHelper;

        public AuthenticationController(IServiceProvider serviceProvider, TokenGenerator tokenGenerator, IConfiguration config, UserService userService, EnumFlagsHelper enumFlagsHelper)
            : base(serviceProvider)
        {
            _tokenGenerator = tokenGenerator;
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
            var userResult = await _userService.AuthenticateUser(loginCredentials.Username, loginCredentials.Password, cancellationToken);

            var actionResult = await userResult.Match<Task<ActionResult>>(f0: async user =>
            {
                string refreshToken = _userService.GenerateRefreshToken();
                await _userService.UpdateUserRefreshTokenAsync(user.Id, refreshToken, cancellationToken);
                return Ok(new Dto.LoginResponse()
                {
                    refreshToken = refreshToken,
                    token = _tokenGenerator.GenerateToken(user)
                });
            },
                f1: async (none) => Unauthorized()
            );

            return actionResult;
        }


        /// <summary>
        /// Refresh your token
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Token use to login</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("refreshToken")]
        public async Task<ActionResult<Dto.Authentication.RefreshTokenResponse>> RefreshToken(Dto.Authentication.RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var userResult = await _userService.GetUserByRefreshTokenAsync(request.RefreshToken, cancellationToken);

            var actionResult = await userResult.Match<Task<ActionResult>>(f0: async user =>
            {
                string refreshToken = _userService.GenerateRefreshToken();
                await _userService.UpdateUserRefreshTokenAsync(user.Id, refreshToken, cancellationToken);
                return Ok(new Dto.LoginResponse()
                {
                    refreshToken = refreshToken,
                    token = _tokenGenerator.GenerateToken(user)
                });
            },
                f1:  async (none) => Unauthorized()
            );

            return actionResult;
        }
    }
}
