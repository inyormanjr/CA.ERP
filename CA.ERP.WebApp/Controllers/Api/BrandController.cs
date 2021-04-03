using AutoMapper;
using CA.ERP.Application.Services;
using CA.ERP.Domain.BrandAgg;
using CA.ERP.Domain.Core.DomainResullts;
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
    public class BrandController : ControllerBase
    {

        private readonly IBrandAppService _brandAppService;
        private readonly IMapper _mapper;

        public BrandController(IBrandAppService brandAppService, IMapper mapper)
        {

            _brandAppService = brandAppService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dto.CreateResponse>> CreateBrand(Dto.Brand.CreateBrandRequest request, CancellationToken cancellationToken)
        {

            var createResult = await _brandAppService.CreateBrandAsync(request.Data.Name, request.Data.Description, cancellationToken: cancellationToken);
            switch (createResult.ErrorType)
            {
                case Domain.Core.DomainResullts.ErrorType.Success:
                    return Ok(createResult.Result);
                case Domain.Core.DomainResullts.ErrorType.Forbidden:
                    return Forbid();
                case Domain.Core.DomainResullts.ErrorType.NotFound:
                    return NotFound();
                default:
                    break;
            }
            return BadRequest(createResult);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, Dto.Brand.UpdateBrandRequest request, CancellationToken cancellationToken)
        {

            DomainResult result = await _brandAppService.UpdateAsync(id, request.Data.Name, request.Data.Description, cancellationToken);

            switch (result.ErrorType)
            {
                case ErrorType.Success:
                    return NoContent();
                case ErrorType.Forbidden:
                    return Forbid();
                case ErrorType.NotFound:
                    return NotFound();
                default:
                    break;
            }
            return BadRequest(result);

        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.GetManyResponse<Dto.Brand.BrandView>>> Get(CancellationToken cancellationToken)
        {
            var brands = await _brandAppService.GetManyAsync(cancellationToken);
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
            DomainResult<Brand> brandResult = await _brandAppService.GetOneAsync(id, cancellationToken);
            switch (brandResult.ErrorType)
            {
                case ErrorType.Success:
                    return Ok(_mapper.Map<Dto.Brand.BrandView>(brandResult.Result));
                case ErrorType.Forbidden:
                    return Forbid();
                case ErrorType.NotFound:
                    return NotFound();
                default:
                    break;
            }

            return BadRequest(brandResult);

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            DomainResult brandResult = await _brandAppService.DeleteAsync(id, cancellationToken);
            switch (brandResult.ErrorType)
            {
                case ErrorType.Success:
                    return Ok();
                case ErrorType.Forbidden:
                    return Forbid();
                case ErrorType.NotFound:
                    return NotFound();
                default:
                    break;
            }
            return BadRequest(brandResult);
            
        }

    }
}
