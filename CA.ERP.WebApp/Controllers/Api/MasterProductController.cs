using AutoMapper;
using CA.ERP.Application.CommandQuery.MasterProductCommandQuery.CreateMasterProduct;
using CA.ERP.Application.CommandQuery.MasterProductCommandQuery.GetManyMasterProduct;
using CA.ERP.Application.CommandQuery.MasterProductCommandQuery.GetOneMasterProduct;
using CA.ERP.Application.CommandQuery.MasterProductCommandQuery.UpdateMasterProduct;
using CA.ERP.Domain.MasterProductAgg;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MasterProductController : ControllerBase
    {

        private readonly ILogger<MasterProductController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MasterProductController(ILogger<MasterProductController> logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Create master product
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.CreateResponse>> Create(Dto.MasterProduct.CreateMasterProductRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateMasterProductCommand(request.Data.Model, request.Data.Description, request.Data.BrandId);
            var createResult = await _mediator.Send(command, cancellationToken);


            if (createResult.IsSuccess)
            {
                var response = new Dto.CreateResponse()
                {
                    Id = createResult.Result
                };

                return Ok(response);
            }
            else
            {
                return BadRequest(createResult);
            }


        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, Dto.UpdateBaseRequest<Dto.MasterProduct.MasterProductUpdate> request, CancellationToken cancellationToken)
        {
            var command = new UpdateMasterProductCommand(id, request.Data.Model, request.Data.Description, request.Data.BrandId, request.Data.ProductStatus);
            var result = await _mediator.Send(command, cancellationToken);
            
            if (result.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                switch (result.ErrorCode)
                {
                    case MasterProductErrorCodes.MasterProductNotFound:
                        return NotFound();
                    default:
                        break;
                }
                _logger.LogError(result.ErrorCode, result);
                return BadRequest(result);
            }


        }

        /// <summary>
        /// Get a list of master products
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.GetManyResponse<Dto.MasterProduct.MasterProductView>>> Get(CancellationToken cancellationToken)
        {
            var query = new GetManyMasterProductQuery();
            var result = await _mediator.Send(query, cancellationToken);

            var dtoMasterProducts = _mapper.Map<List<Dto.MasterProduct.MasterProductView>>(result.Data);
            var response = new Dto.GetManyResponse<Dto.MasterProduct.MasterProductView>()
            {
                Data = dtoMasterProducts,
                TotalCount = result.TotalCount
            };
            return Ok(response);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.MasterProduct.MasterProductView>> Get(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetOneMasterProductQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsSuccess)
            {

                return Ok(result.Result);
            }
            switch (result.ErrorType)
            {
                case Domain.Core.DomainResullts.ErrorType.NotFound:
                    return NotFound();
                default:
                    break;
            }

            return BadRequest(result);
        }
    }
}
