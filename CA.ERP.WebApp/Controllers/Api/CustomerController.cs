using System;
using System.Threading.Tasks;
using CA.ERP.Domain.CustomerAgg;
using CA.ERP.WebApp.Dto;
using CA.ERP.WebApp.Dto.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CA.ERP.WebApp.Controllers.Api
{
    
    public class CustomerController : BaseApiController
    {
    private readonly CustomerService _customerService;

    public CustomerController(IServiceProvider serviceProvider, CustomerService customerService) 
            : base(serviceProvider)
        {
      _customerService = customerService;
    }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CreateResponse>> Post([FromBody] CreateCustomerRequest request)
        {
            var result = await _customerService.CreateCustomer(request.Data.FirstName, request.Data.MiddleName, request.Data.LastName, request.Data.Address, request.Data.Employer , request.Data.EmployerAddress, request.Data.CoMaker, request.Data.CoMakerAddress);
            return result.Match<ActionResult>(
                f0: id => Ok(new CreateResponse() { Id = id}),
                 f1: validationError => {
                     return BadRequest(_mapper.Map<ValidationError>(validationError));
                 }
            );
        }

    }
}