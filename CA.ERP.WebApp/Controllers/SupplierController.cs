using AutoMapper;
using CA.ERP.Domain.SupplierAgg;
using CA.ERP.Domain.UserAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers
{
    /// <summary>
    /// Contains supplier and supllier brands related endpoints
    /// </summary>
    [Authorize]
    public class SupplierController : BaseApiController
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly SupplierService _supplierService;
        private readonly IUserHelper _userHelper;
        private readonly IMapper _mapper;

        public SupplierController(ILogger<SupplierController> logger, SupplierService supplierService, IUserHelper userHelper, IMapper mapper)
        {
            _logger = logger;
            _supplierService = supplierService;
            _userHelper = userHelper;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new supplier
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dto.CreateResponse>> CreateSupplier(Dto.CreateSupplierRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User {0} creating supplier.", _userHelper.GetCurrentUserId());
            var createResult = await _supplierService.CreateSupplierAsync(request.Name, request.Address, request.ContactPerson, cancellationToken: cancellationToken);
            return createResult.Match<ActionResult>(
            f0: (supplierId) =>
            {
                var response = new Dto.CreateResponse()
                {
                    Id = supplierId
                };
                _logger.LogInformation("User {0} supplier branch creation succeeded.", _userHelper.GetCurrentUserId());
                return Ok(response);
            },
            f1: (validationErrors) =>
            {
                var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
                {
                    GeneralError = "Validation Error",
                    ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
                };

                _logger.LogInformation("User {0} supplier branch creation failed.", _userHelper.GetCurrentUserId());
                return BadRequest(response);
            }
         );
        }

        /// <summary>
        /// Updates supplier
        /// </summary>
        /// <param name="id">The id of the supplier to update</param>
        /// <param name="request">The request data</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> UpdateSupplier(Guid id, Dto.UpdateSupplierRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User {0} updating supplier.", _userHelper.GetCurrentUserId());
            var domSupplier = _mapper.Map<Supplier>(request.Data);
            var createResult = await _supplierService.UpdateAsync(id, domSupplier, cancellationToken: cancellationToken);
            return createResult.Match<IActionResult>(
                f0: (supplierId) =>
                {
                    _logger.LogInformation("User {0} supplier branch update succeeded.", _userHelper.GetCurrentUserId());
                    return NoContent();
                },
                f1: (validationErrors) =>
                {
                    var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
                    {
                        GeneralError = "Validation Error",
                        ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
                    };

                    _logger.LogInformation("User {0} supplier branch update failed.", _userHelper.GetCurrentUserId());
                    return BadRequest(response);
                },
                f2: (notFound) => {
                    _logger.LogInformation("User {0} supplier branch update failed. (NotFound)", _userHelper.GetCurrentUserId());
                    return NotFound();
                }
             );
        }
        /// <summary>
        /// Get multiple suppliers
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.GetManyResponse<Dto.Supplier>>> Get(CancellationToken cancellationToken)
        {
            var suppliers = await _supplierService.GetManyAsync(cancellationToken: cancellationToken);
            var dtoSuppliers = _mapper.Map<List<Dto.Supplier>>(suppliers);
            var response = new Dto.GetManyResponse<Dto.Supplier>()
            {
                Data = dtoSuppliers
            };
            return Ok(response);
        }


        /// <summary>
        /// Get single supplier
        /// </summary>
        /// <param name="id">The id of suppler to get.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.Supplier>> Get(Guid id, CancellationToken cancellationToken)
        {
            var supplierOption = await _supplierService.GetOneAsync(id, cancellationToken: cancellationToken);
            return supplierOption.Match<ActionResult>(
                f0: (supplier) =>
                {
                    return Ok(_mapper.Map<Dto.Supplier>(supplier));
                },
                f1: (notFound) => NotFound()
                );
        }




    }
}
