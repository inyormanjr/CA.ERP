using AutoMapper;
using CA.ERP.Domain.BranchAgg;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Dto = CA.ERP.WebApp.Dto;
using Dom = CA.ERP.Domain.BranchAgg;
using OneOf;
using OneOf.Types;
using Microsoft.AspNetCore.Authorization;

namespace CA.ERP.WebApp.Controllers
{
    [Authorize]
    public class BranchController:BaseApiController
    {
        private readonly ILogger<BranchController> _logger;
        private readonly BranchService _branchService;
        private readonly IMapper _mapper;

        public BranchController(ILogger<BranchController> logger, BranchService branchService,IBranchRepository branchRepository, IMapper mapper)
        {
            _logger = logger;
            _branchService = branchService;
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public IBranchRepository _branchRepository { get; }

        /// <summary>
        /// Get multiple branches
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.GetManyResponse<Dto.Branch>>> Get()
        {
            var branches = await _branchService.GetAsync();
            var dtoBranches = _mapper.Map<List<Dto.Branch>>(branches);
            var response = new Dto.GetManyResponse<Dto.Branch>() {
                Data = dtoBranches
            };
            return Ok(response);
        }

        /// <summary>
        /// Create branch
        /// </summary>
        /// <param name="request">The request data</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dto.CreateResponse>> CreateBranch(Dto.CreateBranchRequest request, CancellationToken cancellationToken)
        {

            var createResult = await _branchService.CreateBranchAsync(request.Branch.Name, request.Branch.BranchNo, request.Branch.Code, request.Branch.Address, request.Branch.Contact, cancellationToken);

            return createResult.Match<ActionResult>(
                f0: (brandId) =>
                {
                    var response = new Dto.CreateResponse()
                    {
                        Id = brandId
                    };
                    return Ok(response);
                },
                f1: (none) => BadRequest()
             );
        }

        /// <summary>
        /// Update branch base on Id
        /// </summary>
        /// <param name="id">The branch Id</param>
        /// <param name="request">Updated data</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBranch(Guid id, Dto.UpdateBranchRequest request, CancellationToken cancellationToken)
        {
            var domBranch = _mapper.Map<Dom.Branch>(request.Branch);
            OneOf<Guid, NotFound> result = await _branchService.UpdateAsync(id, domBranch, cancellationToken);

            return result.Match<IActionResult>(
                f0: (branch) => NoContent(),
                f1: (error) => NotFound()
            );
        }

        /// <summary>
        /// Delete the branch base on Id.
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBranch(Guid id, CancellationToken cancellationToken)
        {
            OneOf<Success, NotFound> result = await _branchService.DeleteAsync(id, cancellationToken);
            return result.Match<IActionResult>(
                f0: (success) => NoContent(),
                f1: (notFound) => NotFound()
                );
        }
    }
}
