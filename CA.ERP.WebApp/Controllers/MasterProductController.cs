using AutoMapper;
using CA.ERP.Domain.MasterProductAgg;
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
                var response = new Dto.ErrorResponse()
                {
                    GeneralError = "Validation Error",
                    ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
                };

                _logger.LogInformation("User {0} supplier branch creation failed.", _userHelper.GetCurrentUserId());
                return BadRequest(response);
            });
        }
    }
}
