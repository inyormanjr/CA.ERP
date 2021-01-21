﻿using AutoMapper;
using CA.ERP.Domain.BrandAgg;
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

namespace CA.ERP.WebApp.Controllers.Api
{
    /// <summary>
    /// Handles brand related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandController : BaseApiController
    {
        private readonly BrandService _brandService;

        public BrandController(IServiceProvider serviceProvider, IUserHelper userHelper, BrandService brandService, IMapper mapper)
            : base(serviceProvider)
        {
            _brandService = brandService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dto.CreateResponse>> CreateBrand(Dto.Brand.CreateBrandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User {0} creating brand.", _userHelper.GetCurrentUserId());
            var createResult = await _brandService.CreateBrandAsync(request.Data.Name, request.Data.Description, cancellationToken: cancellationToken);
            return createResult.Match<ActionResult>(
            f0: (supplierId) =>
            {
                var response = new Dto.CreateResponse()
                {
                    Id = supplierId
                };
                _logger.LogInformation("User {0} supplier brand creation succeeded.", _userHelper.GetCurrentUserId());
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
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, Dto.Brand.UpdateBrandRequest request, CancellationToken cancellationToken)
        {
            var domBrand = _mapper.Map<Brand>(request.Data);
            OneOf<Guid, List<ValidationFailure>, NotFound> result = await _brandService.UpdateAsync(id, domBrand, cancellationToken);

            return result.Match<IActionResult>(
                f0: (branch) => NoContent(),
                f1: (validationErrors) => {
                    var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
                    {
                        GeneralError = "Validation Error",
                        ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
                    };

                    _logger.LogInformation("User {0} brand update failed.", _userHelper.GetCurrentUserId());
                    return BadRequest(response);
                },
                f2: (error) => NotFound()
            );
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.GetManyResponse<Dto.Brand.BrandView>>> Get(CancellationToken cancellationToken)
        {
            var brands = await _brandService.GetManyAsync(cancellationToken);
            var dtoBrands = _mapper.Map<List<Dto.Brand.BrandView>>(brands);
            var response = new Dto.GetManyResponse<Dto.Brand.BrandView>()
            {
                Data = dtoBrands
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dto.Brand.BrandView>> Get(Guid id, CancellationToken cancellationToken)
        {
            var brandOption = await _brandService.GetOneAsync(id, cancellationToken);
            return brandOption.Match<ActionResult>(
                f0: brand =>
                {
                    return Ok(_mapper.Map<Dto.Brand.BrandView>(brand));
                },
                f1: notfound => NotFound()
            );
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var brandOption = await _brandService.DeleteAsync(id, cancellationToken);
            return brandOption.Match<ActionResult>(
                f0: Success =>
                {
                    return NoContent();
                },
                f1: notfound => NotFound()
            );
        }

    }
}