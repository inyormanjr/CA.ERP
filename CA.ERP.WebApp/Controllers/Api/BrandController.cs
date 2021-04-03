using AutoMapper;
using CA.ERP.Application.CommandQuery.BrandCommandQuery.CreateBrand;
using CA.ERP.Application.CommandQuery.BrandCommandQuery.DeleteBrand;
using CA.ERP.Application.CommandQuery.BrandCommandQuery.GetManyBrand;
using CA.ERP.Application.CommandQuery.BrandCommandQuery.GetOneBrand;
using CA.ERP.Application.CommandQuery.BrandCommandQuery.UpdateBrand;
using CA.ERP.Application.Services;
using CA.ERP.Domain.BrandAgg;
using CA.ERP.Domain.Core.DomainResullts;
using FluentValidation.Results;
using MediatR;
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
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BrandController(IMediator mediator, IMapper mapper)
        {

            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dto.CreateResponse>> CreateBrand(Dto.Brand.CreateBrandRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateBrandCommand(request.Data.Name, request.Data.Description);
            var createResult = await _mediator.Send(command, cancellationToken);
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
            var command = new UpdateBrandCommand(id, request.Data.Name, request.Data.Description);
            var result = await _mediator.Send(command, cancellationToken);

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
            var query = new GetManyBrandQuery();
            var result = await _mediator.Send(query, cancellationToken);

            var dtoBrands = _mapper.Map<List<Dto.Brand.BrandView>>(result.Data);
            var response = new Dto.GetManyResponse<Dto.Brand.BrandView>()
            {
                Data = dtoBrands,
                TotalCount = result.TotalCount
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dto.Brand.BrandView>> Get(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetOneBrandQuery(id);
            var result = await _mediator.Send(query, cancellationToken);


            switch (result.ErrorType)
            {
                case ErrorType.Success:
                    return Ok(_mapper.Map<Dto.Brand.BrandView>(result.Result));
                case ErrorType.Forbidden:
                    return Forbid();
                case ErrorType.NotFound:
                    return NotFound();
                default:
                    break;
            }

            return BadRequest(result);

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteBrandCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            switch (result.ErrorType)
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
            return BadRequest(result);
            
        }

    }
}
