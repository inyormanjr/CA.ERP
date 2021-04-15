using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.User;
using CA.Identity.Models;
using CA.Identity.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserRepository _userRepository;


        public UserController(UserManager<ApplicationUser> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post(UserCreateRequest userCreateRequest, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser()
            {
                UserName = userCreateRequest.Data.UserName,
                FirstName = userCreateRequest.Data.FirstName,
                LastName = userCreateRequest.Data.LastName,
            };
            var result = await _userManager.CreateAsync(user, userCreateRequest.Data.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
