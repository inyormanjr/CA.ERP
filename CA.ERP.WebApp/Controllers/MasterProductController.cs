using AutoMapper;
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

namespace CA.ERP.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MasterProductController : BaseApiController
    {
        private readonly ILogger<MasterProductController> _logger;
        private readonly MasterProductService _masterProductService;
        private readonly IMapper _mapper;
        private readonly IUserHelper _userHelper;

        public MasterProductController(ILogger<MasterProductController> logger, MasterProductService masterProductService, IMapper mapper, IUserHelper userHelper)
        {
            _logger = logger;
            _masterProductService = masterProductService;
            _mapper = mapper;
            _userHelper = userHelper;
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
        public async Task<ActionResult<Dto.CreateResponse>> Create(Dto.CreateMasterProductRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User {0} creating masterproduct.", _userHelper.GetCurrentUserId());
            var createResult = await _masterProductService.CreateMasterProduct(request.Model, request.Description, (ProductStatus)(int)request.ProductStatus, request.BrandId, cancellationToken: cancellationToken);
            return createResult.Match<ActionResult>(
            f0: (id) =>
            {
                var response = new Dto.CreateResponse()
                {
                    Id = id
                };
                _logger.LogInformation("User {0} masterproduct creation succeeded.", _userHelper.GetCurrentUserId());
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
        public async Task<IActionResult> Update(Guid id, Dto.UpdateBaseRequest<Dto.MasterProduct> request, CancellationToken cancellationToken)
        {
            var domData = _mapper.Map<MasterProduct>(request.Data);
            OneOf<Guid, List<ValidationFailure>, NotFound> result = await _masterProductService.UpdateAsync(id, domData, cancellationToken);

            return result.Match<IActionResult>(
                f0: (masterProduct) => NoContent(),
                f1: (validationErrors) => {
                    var response = new Dto.ErrorResponse(HttpContext.TraceIdentifier)
                    {
                        GeneralError = "Validation Error",
                        ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
                    };

                    _logger.LogInformation("User {0} brand update failed.", _userHelper.GetCurrentUserId());
                    return BadRequest(response);
                },
                f2: (notFound) => NotFound()
            );
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.GetManyResponse<Dto.MasterProduct>>> Get(CancellationToken cancellationToken)
        {
            var masterProducts = await _masterProductService.GetManyAsync(cancellationToken);
            var dtoMasterProducts = _mapper.Map<List<Dto.MasterProduct>>(masterProducts);
            var response = new Dto.GetManyResponse<Dto.MasterProduct>()
            {
                Data = dtoMasterProducts
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.MasterProduct>> Get(Guid id, CancellationToken cancellationToken)
        {
            var masterProductOption = await _masterProductService.GetOneAsync(id ,cancellationToken);
            
            return masterProductOption.Match<ActionResult>(
                f0: masterProduct => Ok(masterProduct),
                f1: notfound => NotFound()
                );
        }
    }
}
