using AutoMapper;
using CA.ERP.Application.CommandQuery.SupplierCommandQuery.GetManySupplier;
using CA.ERP.Application.CommandQuery.SupplierCommandQuery.GetManySupplierBrand;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.SupplierAgg;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.Supplier;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dto = CA.ERP.Shared.Dto;

namespace CA.ERP.WebApp.Controllers.Api
{
    /// <summary>
    /// Contains supplier and supllier brands related endpoints
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SupplierController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SupplierController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new supplier
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult<Dto.Supplier.CreateSupplierResponse>> CreateSupplier(Dto.Supplier.CreateSupplierRequest request, CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation("User {0} creating supplier.", _userHelper.GetCurrentUserId());
        //    var supplierBrands = _mapper.Map<List<SupplierBrand>>(request.Data.SupplierBrands);
        //    var createResult = await _supplierService.CreateSupplierAsync(request.Data.Name, request.Data.Address, request.Data.ContactPerson, supplierBrands, cancellationToken: cancellationToken);
        //    return createResult.Match<ActionResult>(
        //    f0: (supplier) =>
        //    {
        //        var response = new Dto.Supplier.CreateSupplierResponse()
        //        {
        //            Id = supplier.Id,
        //            Name = supplier.Name,
        //            Address = supplier.Address,
        //            ContactPerson = supplier.ContactPerson
        //        };
        //        _logger.LogInformation("User {0} supplier branch creation succeeded.", _userHelper.GetCurrentUserId());
        //        return Ok(response);
        //    },
        //    f1: (validationErrors) =>
        //    {
        //        var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
        //        {
        //            GeneralError = "Validation Error",
        //            ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
        //        };

        //        _logger.LogInformation("User {0} supplier branch creation failed.", _userHelper.GetCurrentUserId());
        //        return BadRequest(response);
        //    }
        // );
        //}

        /// <summary>
        /// Updates supplier
        /// </summary>
        /// <param name="id">The id of the supplier to update</param>
        /// <param name="request">The request data</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        //[Authorize]
        //public async Task<IActionResult> UpdateSupplier(Guid id, Dto.Supplier.UpdateSupplierRequest request, CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation("User {0} updating supplier.", _userHelper.GetCurrentUserId());
        //    var domSupplier = _mapper.Map<Supplier>(request.Data);
        //    var createResult = await _supplierService.UpdateAsync(id, domSupplier, cancellationToken: cancellationToken);
        //    return createResult.Match<IActionResult>(
        //        f0: (supplierId) =>
        //        {
        //            _logger.LogInformation("User {0} supplier branch update succeeded.", _userHelper.GetCurrentUserId());
        //            return NoContent();
        //        },
        //        f1: (validationErrors) =>
        //        {
        //            var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
        //            {
        //                GeneralError = "Validation Error",
        //                ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
        //            };

        //            _logger.LogInformation("User {0} supplier branch update failed.", _userHelper.GetCurrentUserId());
        //            return BadRequest(response);
        //        },
        //        f2: (notFound) => {
        //            _logger.LogInformation("User {0} supplier branch update failed. (NotFound)", _userHelper.GetCurrentUserId());
        //            return NotFound();
        //        }
        //     );
        //}


        /// <summary>
        /// Get multiple suppliers
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaginatedResponse<Dto.Supplier.SupplierView>>> Get(string name, int skip = 0, int take = 10, CancellationToken cancellationToken = default)
        {
            var query = new GetManySupplierQuery(name, skip, take);
            var result = await _mediator.Send(query, cancellationToken);


            var dtoSuppliers = _mapper.Map<List<Dto.Supplier.SupplierView>>(result.Data);
            var response = new Dto.PaginatedResponse<Dto.Supplier.SupplierView>()
            {
                Data = dtoSuppliers,
                TotalCount = result.TotalCount
            };
            return Ok(response);
        }


        /// <summary>
        /// Get single supplier
        /// </summary>
        /// <param name="supplierId">The id of suppler to get.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<Dto.Supplier.SupplierView>> Get(Guid id, CancellationToken cancellationToken)
        //{
        //    var supplierOption = await _supplierService.GetOneAsync(id, cancellationToken: cancellationToken);
        //    return supplierOption.Match<ActionResult>(
        //        f0: (supplier) =>
        //        {
        //            return Ok(_mapper.Map<Dto.Supplier.SupplierView>(supplier));
        //        },
        //        f1: (notFound) => NotFound()
        //        );
        //}


        //[HttpPut("{id}/MasterProduct")]
        //[HttpPost("{id}/MasterProduct")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> UpdateSupplierMasterProduct(Guid id, Dto.UpdateBaseRequest<SupplierMasterProductUpdate> request, CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation("User {0} updating supplier master product.", _userHelper.GetCurrentUserId());

        //    var option = await _supplierService.AddOrUpdateSupplierMasterProductAsync(id, request.Data.MasterProductId, request.Data.CostPrice, cancellationToken);
        //    return option.Match<IActionResult>(
        //        f0: success => NoContent(),
        //        f1: (validationErrors) =>
        //        {
        //            var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
        //            {
        //                GeneralError = "Validation Error",
        //                ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
        //            };

        //            _logger.LogInformation("User {0} supplier master product update failed.", _userHelper.GetCurrentUserId());
        //            return BadRequest(response);
        //        }
        //    );

        //}

        //[HttpPut("{id}/Brand")]
        //[HttpPost("{id}/Brand")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> UpdateSupplierBrand(Guid id, Dto.UpdateBaseRequest<SupplierBrandUpdate> request, CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation("User {0} updating supplier brand product.", _userHelper.GetCurrentUserId());

        //    var option = await _supplierService.AddSupplierBrand(id, request.Data.BrandId, cancellationToken);
        //    return option.Match<IActionResult>(
        //        f0: success => NoContent(),
        //        f1: (validationErrors) =>
        //        {
        //            var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
        //            {
        //                GeneralError = "Validation Error",
        //                ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
        //            };

        //            _logger.LogInformation("User {0} supplier supplier brand update failed.", _userHelper.GetCurrentUserId());
        //            return BadRequest(response);
        //        },
        //        f2: notfound => NotFound()
        //    );

        //}

        //[HttpDelete("{id}/Brand/{brandId}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> DeleteSupplierBrand(Guid id, Guid brandId, CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation("User {0} deleting supplier brand.", _userHelper.GetCurrentUserId());

        //    var option = await _supplierService.DeleteSupplierBrandAsync(id, brandId, cancellationToken);
        //    return option.Match<IActionResult>(
        //        f0: success => NoContent(),
        //        f1: notfound => NotFound()
        //    );

        //}

        /// <summary>
        /// Get a lite version of supplier brands with master products and cost
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{supplierId}/Brand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<SupplierBrandView>>> GetSupplierBrands(Guid supplierId, CancellationToken cancellationToken)
        {
            var query = new GetManySupplierBrandQuery(supplierId);
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(_mapper.Map<List<SupplierBrandView>>(result));

        }

    }
}
