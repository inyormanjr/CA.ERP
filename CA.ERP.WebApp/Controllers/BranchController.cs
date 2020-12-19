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

namespace CA.ERP.WebApp.Controllers
{
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

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.GetBranchResponse>> Get()
        {
            
            var branches = await _branchService.GetAsync();
            var dtoBranches = _mapper.Map<List<Dto.Branch>>(branches);
            var response = new Dto.GetBranchResponse() {
                Branches = dtoBranches
            };
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.CreateBranchResponse>> CreateBranch(Dto.CreateBranchRequest request, CancellationToken cancellationToken)
        {

            var createResult = await _branchService.CreateBranchAsync(request.Branch.Name, request.Branch.BranchNo, request.Branch.Code, request.Branch.Address, request.Branch.Contact, cancellationToken);

            return createResult.Match<ActionResult>(
                f0: (branch) =>
                {
                    var response = new Dto.CreateBranchResponse()
                    {
                        Branch = _mapper.Map<Dto.Branch>(branch)
                    };
                    return Ok(response);
                },
                f1: (none) => BadRequest()
             );
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBranch(string id, Dto.UpdateBranchRequest request, CancellationToken cancellationToken)
        {
            var domBranch = _mapper.Map<Dom.Branch>(request.Branch);
            OneOf<Branch, NotFound> result = await _branchService.UpdateAsync(id, domBranch, cancellationToken);

            return result.Match<IActionResult>(
                f0: (branch) => NoContent(),
                f1: (error) => NotFound()
            );
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBranch(string id, CancellationToken cancellationToken)
        {
            OneOf<Success, NotFound> result = await _branchService.DeleteAsync(id, cancellationToken);
            return result.Match<IActionResult>(
                f0: (success) => NoContent(),
                f1: (notFound) => NotFound()
                );
        }
    }
}
