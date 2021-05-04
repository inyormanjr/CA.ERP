using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.User;
using CA.Identity.Data;
using CA.Identity.Models;
using CA.Identity.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CA.Identity.Controllers
{

    [Authorize(LocalApi.PolicyName)]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserRepository userRepository, ApplicationDbContext applicationDbContext, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }
        //GET: api/<UserController>
        [HttpGet]
        public async Task<PaginatedResponse<UserView>> Get(string firstName, string lastName, string userName, int skip = 0, int take = 100, CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.GetUsers(firstName, lastName, userName, skip, take, cancellationToken);
            var ret = new List<UserView>();

            foreach (var user in users)
            {
                var dtoUser = new UserView()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.UserName,
                    UserBranches = user.UserBranches.Select(ub => new UserBranchView()
                    {
                        BranchId = ub.BranchId,
                        UserId = ub.UserId,
                        Name = ub.BranchName,
                        Code = ub.BranchCode,
                    }).ToList(),
                    Roles = (await _userManager.GetRolesAsync(user)).ToList()
                };

                ret.Add(dtoUser);
            }
            return new PaginatedResponse<UserView>()
            {
                TotalCount = users.Count,
                Data = ret
            };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            var dtoUser = new UserView()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                UserBranches = user.UserBranches.Select(ub => new UserBranchView()
                {
                    BranchId = ub.BranchId,
                    UserId = ub.UserId,
                    Name = ub.BranchName,
                    Code = ub.BranchCode,
                }).ToList(),
                Roles = (await _userManager.GetRolesAsync(user)).ToList()
            };
            return Ok(dtoUser);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post(UserCreateRequest userCreateRequest, CancellationToken cancellationToken)
        {
            using (var transaction = _applicationDbContext.Database.BeginTransaction())
            {

                try
                {
                    var user = new ApplicationUser()
                    {
                        UserName = userCreateRequest.Data.UserName,
                        FirstName = userCreateRequest.Data.FirstName,
                        LastName = userCreateRequest.Data.LastName,
                        Roles = userCreateRequest.Data.Roles.ToList(),
                        UserBranches = userCreateRequest.Data.Branches.Select(b => new UserBranch() { BranchId = b.BranchId.ToString(), BranchName = b.Name }).ToList()
                    };
                    var result = await _userManager.CreateAsync(user, userCreateRequest.Data.Password);

                    if (!result.Succeeded)
                    {
                        _logger.LogError("Saving user failed. Errors : {Errors}", result.Errors.Select(ie => ie.Code));
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                        
                        return this.ValidationProblem();
                    }

                    foreach (var role in user.Roles)
                    {
                        var addRoleResult = await _userManager.AddToRoleAsync(user, role);
                        if (!addRoleResult.Succeeded)
                        {
                            _logger.LogError("Failed adding role. Errors : {Errors}", addRoleResult.Errors);
                            foreach (var error in addRoleResult.Errors)
                            {
                                ModelState.AddModelError(error.Code, error.Description);
                            }

                            return this.ValidationProblem();
                        }
                    }



                    await transaction.CommitAsync();
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }

                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
                
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}/password")]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] UpdateBaseRequest<UserChangePassword> updatePasswordRequest)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var passwordRestToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, passwordRestToken, updatePasswordRequest.Data.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UpdateBaseRequest<UserUpdate> userUpdateRequest)
        {
            using (var transaction = _applicationDbContext.Database.BeginTransaction())
            {

                try
                {
                    ApplicationUser user = await _userRepository.GetUserById(id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    user.UserName = userUpdateRequest.Data.UserName;
                    user.FirstName = userUpdateRequest.Data.FirstName;
                    user.LastName = userUpdateRequest.Data.LastName;

                    user.UserBranches.Clear();

                    user.UserBranches = userUpdateRequest.Data.Branches.Select(b => new UserBranch() { BranchId = b.BranchId.ToString(), BranchName = b.Name }).ToList();

                    var result = await _userManager.UpdateAsync(user);

                    if (!result.Succeeded)
                    {
                        _logger.LogError("Saving user failed. Errors : {Errors}", result.Errors.Select(ie => ie.Code));
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }

                        return this.ValidationProblem();
                    }

                    var currentRoles = await _userManager.GetRolesAsync(user);

                    result = await _userManager.RemoveFromRolesAsync(user, currentRoles);

                    if (!result.Succeeded)
                    {
                        _logger.LogError("Clearing roles. Errors : {Errors}", result.Errors.Select(ie => ie.Code));
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }

                        return this.ValidationProblem();
                    }


                    foreach (var role in userUpdateRequest.Data.Roles)
                    {
                        var addRoleResult = await _userManager.AddToRoleAsync(user, role);
                        if (!addRoleResult.Succeeded)
                        {
                            _logger.LogError("Failed adding role. Errors : {Errors}", addRoleResult.Errors);
                            foreach (var error in addRoleResult.Errors)
                            {
                                ModelState.AddModelError(error.Code, error.Description);
                            }

                            return this.ValidationProblem();
                        }
                    }



                    await transaction.CommitAsync();
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }

                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }

            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
