using CA.ERP.WebApp.Dto.User;
using CA.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CA.Identity.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {

    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;
    }
    // GET: api/<UserController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
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
      var user = new ApplicationUser() {
        UserName = userCreateRequest.Data.Username,
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
