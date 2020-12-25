using AutoMapper;
using CA.ERP.Domain.BrandAgg;
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
    /// Handles brand related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : BaseApiController
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IUserHelper _userHelper;
        private readonly BrandService _brandService;
        private readonly IMapper _mapper;

        public BrandController(ILogger<BrandController> logger, IUserHelper userHelper, BrandService brandService, IMapper mapper)
        {
            _logger = logger;
            _userHelper = userHelper;
            _brandService = brandService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dto.ErrorResponse), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dto.CreateResponse>> CreateBrand(Dto.CreateBrandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User {0} creating brand.", _userHelper.GetCurrentUserId());
            var createResult = await _brandService.CreateBrandAsync(request.Name, request.Description, cancellationToken: cancellationToken);
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
                var response = new Dto.ErrorResponse()
                {
                    GeneralError = "Validation Error",
                    ValidationErrors = _mapper.Map<List<Dto.ValidationError>>(validationErrors)
                };

                _logger.LogInformation("User {0} supplier branch creation failed.", _userHelper.GetCurrentUserId());
                return BadRequest(response);
            }
         );
        }

    }
}
