using System;
using System.Linq;
using System.Threading;
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
        public async Task<ActionResult<CreateResponse>> Post([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var result = await _customerService.CreateCustomer(request.Data.FirstName, request.Data.MiddleName, request.Data.LastName, request.Data.Address, request.Data.Employer , request.Data.EmployerAddress, request.Data.CoMaker, request.Data.CoMakerAddress, cancellationToken);
            return result.Match<ActionResult>(
                f0: id => Ok(new CreateResponse() { Id = id}),
                 f1: validationError => {
                     return BadRequest(_mapper.Map<ValidationError>(validationError));
                 }
            );
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request.Data);
            var result = await _customerService.UpdateAsync(id, customer, cancellationToken);
            return result.Match<ActionResult>(
                f0: id => NoContent(),
                 f1: validationError => {
                     return BadRequest(_mapper.Map<ValidationError>(validationError));
                 },
                 f2: _ => NotFound()
            );
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dto.GetManyResponse<Dto.Customer.CustomerView>>> Get(string primaryField = null, string firstName = null, string lastname = null, int pageSize = 10, int page = 1, CancellationToken cancellationToken = default)
        {
            lastname = lastname ?? primaryField;
            var result = await _customerService.GetManyAsync(firstName, lastname, pageSize, page, cancellationToken);
            var response = new Dto.GetManyResponse<Dto.Customer.CustomerView>()
            {
                CurrentPage = result.CurrentPage,
                TotalPage = result.TotalPage,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
                Data = result.Data.Select(d => _mapper.Map<Dto.Customer.CustomerView>(d))
            };
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<ActionResult<Dto.Customer.CustomerView>> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _customerService.GetOneAsync(id, cancellationToken);
            return result.Match<ActionResult>(f0: customer => Ok(_mapper.Map<Dto.Customer.CustomerView>(customer)), f1: _ => NotFound());
        }

    }
}