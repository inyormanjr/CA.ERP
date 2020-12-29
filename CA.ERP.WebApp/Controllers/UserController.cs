using AutoMapper;
using CA.ERP.Domain.UserAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public UserController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("Register")]
        public async Task<ActionResult<Dto.RegisterResponse>> Register(Dto.RegisterRequest request, CancellationToken cancellationToken)
        {

            var result = await _userService.CreateUserAsync(request.UserName, request.Password, (UserRole)(int)request.Role, request.FirstName, request.LastName, request.Branches, cancellationToken: cancellationToken);

            //change to proper dto 
            return result.Match<ActionResult>(
                f0: userId => Ok(new Dto.RegisterResponse() { UserId = userId }),
                f1: validationFailures => BadRequest(new Dto.ErrorResponse() { GeneralError = "Validation Error", ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationFailures) }),
                f2: error => BadRequest(new Dto.ErrorResponse() { GeneralError = error.Value })
                );
        }
    }
}
