using AutoMapper;
using CA.ERP.Domain.SupplierAgg;
using CA.ERP.Domain.UserAgg;
using CA.ERP.WebApp.Dto.Supplier;
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
        private readonly SupplierService _supplierService;

        public SupplierController(IServiceProvider serviceProvider, SupplierService supplierService, IUserHelper userHelper, IMapper mapper)
            : base(serviceProvider)
        {
            _supplierService = supplierService;
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
        public async Task<ActionResult<Dto.Supplier.CreateSupplierResponse>> CreateSupplier(Dto.Supplier.CreateSupplierRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User {0} creating supplier.", _userHelper.GetCurrentUserId());
            var supplierBrands = _mapper.Map<List<SupplierBrand>>(request.Data.SupplierBrands);
            var createResult = await _supplierService.CreateSupplierAsync(request.Data.Name, request.Data.Address, request.Data.ContactPerson, supplierBrands, cancellationToken: cancellationToken);
            return createResult.Match<ActionResult>(
            f0: (supplier) =>
            {
                var response = new Dto.Supplier.CreateSupplierResponse()
                {
                    Id = supplier.Id,
                    Name = supplier.Name,
                    Address = supplier.Address,
                    ContactPerson = supplier.ContactPerson
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
        public async Task<IActionResult> UpdateSupplier(Guid id, Dto.Supplier.UpdateSupplierRequest request, CancellationToken cancellationToken)
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
        public async Task<ActionResult<Dto.GetManyResponse<Dto.Supplier.SupplierView>>> Get(CancellationToken cancellationToken)
        {
            var suppliers = await _supplierService.GetManyAsync(cancellationToken: cancellationToken);
            var dtoSuppliers = _mapper.Map<List<Dto.Supplier.SupplierView>>(suppliers);
            var response = new Dto.GetManyResponse<Dto.Supplier.SupplierView>()
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
        public async Task<ActionResult<Dto.Supplier.SupplierView>> Get(Guid id, CancellationToken cancellationToken)
        {
            var supplierOption = await _supplierService.GetOneAsync(id, cancellationToken: cancellationToken);
            return supplierOption.Match<ActionResult>(
                f0: (supplier) =>
                {
                    return Ok(_mapper.Map<Dto.Supplier.SupplierView>(supplier));
                },
                f1: (notFound) => NotFound()
                );
        }


        [HttpPut("{id}/MasterProduct")]
        [HttpPost("{id}/MasterProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSupplierMasterProduct(Guid id, Dto.UpdateBaseRequest<SupplierMasterProductUpdate> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User {0} updating supplier master product.", _userHelper.GetCurrentUserId());

            var option = await _supplierService.AddOrUpdateSupplierMasterProductAsync(id, request.Data.MasterProductId, request.Data.CostPrice, cancellationToken);
            return option.Match<IActionResult>(
                f0: success => NoContent(),
                f1: (validationErrors) =>
                {
                    var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
                    {
                        GeneralError = "Validation Error",
                        ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
                    };

                    _logger.LogInformation("User {0} supplier master product update failed.", _userHelper.GetCurrentUserId());
                    return BadRequest(response);
                }
            );
            
        }

        [HttpPut("{id}/Brand")]
        [HttpPost("{id}/Brand")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSupplierBrand(Guid id, Dto.UpdateBaseRequest<SupplierBrandUpdate> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User {0} updating supplier brand product.", _userHelper.GetCurrentUserId());

            var option = await _supplierService.AddSupplierBrand(id, request.Data.BrandId, cancellationToken);
            return option.Match<IActionResult>(
                f0: success => NoContent(),
                f1: (validationErrors) =>
                {
                    var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
                    {
                        GeneralError = "Validation Error",
                        ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
                    };

                    _logger.LogInformation("User {0} supplier supplier brand update failed.", _userHelper.GetCurrentUserId());
                    return BadRequest(response);
                },
                f2: notfound => NotFound()
            );

        }

        [HttpDelete("{id}/Brand/{brandId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSupplierBrand(Guid id, Guid brandId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User {0} deleting supplier brand.", _userHelper.GetCurrentUserId());

            var option = await _supplierService.DeleteSupplierBrandAsync(id, brandId, cancellationToken);
            return option.Match<IActionResult>(
                f0: success => NoContent(),
                f1: notfound => NotFound()
            );

        }

        /// <summary>
        /// Get a lite version of supplier brands with master products and cost
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}/Brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.GetManyResponse<SupplierBrandLite>>> GetSupplierBrands(Guid id, CancellationToken cancellationToken)
        {
            var supplierBrands = await _supplierService.GetSupplierBrandsAsync(id, cancellationToken: cancellationToken);
            return Ok(new Dto.GetManyResponse<SupplierBrandLite>() {Data = supplierBrands });
            
        }

    }
}
