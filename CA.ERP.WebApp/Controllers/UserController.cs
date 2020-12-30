using AutoMapper;
using CA.ERP.Domain.UserAgg;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
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
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        private readonly IUserHelper _userHelper;

        public UserController(ILogger<UserController> logger, UserService userService, IMapper mapper, IUserHelper userHelper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
            _userHelper = userHelper;
        }
        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dto.CreateResponse>> Register(Dto.User.UserCreateRequest request, CancellationToken cancellationToken)
        {

            var result = await _userService.CreateUserAsync(request.Data.UserName, request.Data.Password, (UserRole)(int)request.Data.Role, request.Data.FirstName, request.Data.LastName, request.Data.Branches, cancellationToken: cancellationToken);

            //change to proper dto 
            return result.Match<ActionResult>(
                f0: userId => Ok(new Dto.CreateResponse() { Id = userId }),
                f1: validationFailures => BadRequest(new Dto.ErrorResponse(HttpContext.TraceIdentifier) { GeneralError = "Validation Error", ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationFailures) }),
                f2: error => BadRequest(new Dto.ErrorResponse(HttpContext.TraceIdentifier) { GeneralError = error.Value })
                );
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(Guid id, Dto.UpdateBaseRequest<Dto.User.UserUpdate> request, CancellationToken cancellationToken)
        {

            var domUser = _mapper.Map<User>(request.Data);
            OneOf<Guid, List<ValidationFailure>, NotFound> createResult = await _userService.UpdateUser(id, domUser, request.Data.Branches, cancellationToken: cancellationToken);
            return createResult.Match<IActionResult>(
                f0: (userId) =>
                {
                    _logger.LogInformation("User {0} user update succeeded.", _userHelper.GetCurrentUserId());
                    return NoContent();
                },
                f1: (validationErrors) =>
                {
                    var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
                    {
                        GeneralError = "Validation Error",
                        ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
                    };

                    _logger.LogInformation("User {0} user update failed.", _userHelper.GetCurrentUserId());
                    return BadRequest(response);
                },
                f2: (notFound) => {
                    _logger.LogInformation("User {0} user update failed. (NotFound)", _userHelper.GetCurrentUserId());
                    return NotFound();
                }
             );
        }

        [HttpPut("{id}/Password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserPassword(Guid id, Dto.UpdateBaseRequest<Dto.User.PasswordUpdateRequest>  request, CancellationToken cancellationToken)
        {

            OneOf<Success, List<ValidationFailure>, NotFound> createResult = await _userService.UpdateUserPassword(id, request.Data.Password, request.Data.ConfirmPassword, cancellationToken: cancellationToken);
            return createResult.Match<IActionResult>(
                f0: (success) =>
                {
                    _logger.LogInformation("User {0} password update succeeded.", _userHelper.GetCurrentUserId());
                    return NoContent();
                },
                f1: (validationErrors) =>
                {
                    var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
                    {
                        GeneralError = "Validation Error",
                        ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
                    };

                    _logger.LogInformation("User {0} password update failed.", _userHelper.GetCurrentUserId());
                    return BadRequest(response);
                },
                f2: (notFound) => {
                    _logger.LogInformation("User {0} password update failed. (NotFound)", _userHelper.GetCurrentUserId());
                    return NotFound();
                }
             );
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var userOption = await _userService.DeleteAsync(id, cancellationToken);
            return userOption.Match<ActionResult>(
                f0: Success =>
                {
                    return NoContent();
                },
                f1: notfound => NotFound()
            );
        }


        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.GetManyResponse<Dto.User.UserView>>> Get(CancellationToken cancellationToken)
        {
            var users = await _userService.GetManyAsync(cancellationToken: cancellationToken);
            var dtoUsers = _mapper.Map<List<Dto.User.UserView>>(users);
            var response = new Dto.GetManyResponse<Dto.User.UserView>()
            {
                Data = dtoUsers
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.User.UserView>> Get(Guid id, CancellationToken cancellationToken)
        {
            var userOption = await _userService.GetOneAsync(id, cancellationToken: cancellationToken);
            return userOption.Match<ActionResult>(
                f0: user => Ok(_mapper.Map<Dto.User.UserView>(user)),
                f1: notFound => NotFound()
                ); 
        }
    }
}
