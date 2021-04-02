using AutoMapper;
using CA.ERP.Application.Services;
using CA.ERP.Domain.MasterProductAgg;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MasterProductController : BaseApiController
    {
        private readonly IMasterProductAppService _masterProductAppService;

        public MasterProductController(IServiceProvider serviceProvider, IMasterProductAppService masterProductAppService)
            :base(serviceProvider)
        {
            _masterProductAppService = masterProductAppService;
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dto.CreateResponse>> Create(Dto.MasterProduct.CreateMasterProductRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User {0} creating masterproduct.", _userHelper.GetCurrentUserId());
            var createResult = await _masterProductAppService.CreateMasterProduct(request.Data.Model, request.Data.Description, request.Data.BrandId, cancellationToken: cancellationToken);
            var response = new Dto.CreateResponse()
            {
                Id = createResult
            };
            _logger.LogInformation("User {0} masterproduct creation succeeded.", _userHelper.GetCurrentUserId());
            return Ok(response);

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, Dto.UpdateBaseRequest<Dto.MasterProduct.MasterProductUpdate> request, CancellationToken cancellationToken)
        {
            try
            {
                await _masterProductAppService.UpdateAsync(id, request.Data.Model, request.Data.Description, request.Data.BrandId, request.Data.ProductStatus, cancellationToken);
                return NoContent();
            }
            catch (MasterProductException ex)
            {
                switch (ex.Code)
                {
                    case MasterProductException.MasterProductNotFound:
                        return NotFound(ex.Message);
                    default:
                        break;
                }
                throw;
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
            var masterProducts = await _masterProductAppService.GetManyAsync(cancellationToken);
            var dtoMasterProducts = _mapper.Map<List<Dto.MasterProduct.MasterProductView>>(masterProducts);
            var response = new Dto.GetManyResponse<Dto.MasterProduct.MasterProductView>()
            {
                Data = dtoMasterProducts
            };
            return Ok(response);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.MasterProduct.MasterProductView>> Get(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var masterProduct = await _masterProductAppService.GetOneAsync(id, cancellationToken);
                return Ok(masterProduct);
            }
            catch (MasterProductException ex)
            {
                switch (ex.Code)
                {
                    case MasterProductException.MasterProductNotFound:
                        return NotFound(ex.Message);
                    default:
                        break;
                }
                throw;
            }
            

        }
    }
}
