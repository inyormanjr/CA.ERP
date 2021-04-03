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
using CA.ERP.WebApp.Dto;
using CA.ERP.Application.Services;
using CA.ERP.Domain.Core.DomainResullts;

namespace CA.ERP.WebApp.Controllers.Api
{
    [Authorize]
    public class BranchController:BaseApiController
    {
        private readonly IBranchAppService _branchAppService;

        public BranchController(IServiceProvider serviceProvider, IBranchAppService branchAppService)
            : base(serviceProvider)
        {
            _branchAppService = branchAppService;
        }


        /// <summary>
        /// Get multiple branches
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.GetManyResponse<Dto.Branch.BranchView>>> Get()
        {
            var branches = await _branchAppService.GetManyAsync();
            var dtoBranches = _mapper.Map<List<Dto.Branch.BranchView>>(branches);
            var response = new Dto.GetManyResponse<Dto.Branch.BranchView>() {
                Data = dtoBranches
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dto.Branch.BranchView>> Get(Guid id, CancellationToken cancellationToken)
        {
            DomainResult<Branch> result = await _branchAppService.GetOneAsync(id, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(_mapper.Map<Dto.Branch.BranchView>(result.Result));
            }
            switch (result.ErrorType)
            {
                case ErrorType.Success:
                    break;
                case ErrorType.Forbidden:
                    return Forbid();
                case ErrorType.NotFound:
                    return NotFound();
                default:
                    break;
            }
            return BadRequest(result);

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
        public async Task<ActionResult<Dto.CreateResponse>> CreateBranch(Dto.Branch.CreateBranchRequest request, CancellationToken cancellationToken)
        {

            DomainResult<Guid> createResult = await _branchAppService.CreateBranchAsync(request.Data.Name, request.Data.BranchNo, request.Data.Code, request.Data.Address, request.Data.Contact, cancellationToken);
            if (createResult.IsSuccess)
            {
                var response = new Dto.CreateResponse()
                {
                    Id = createResult.Result
                };
                return Ok(response);
            }
            switch (createResult.ErrorType)
            {
                case Domain.Core.DomainResullts.ErrorType.Success:
                    break;
                case Domain.Core.DomainResullts.ErrorType.Forbidden:
                    return Forbid();

                case Domain.Core.DomainResullts.ErrorType.NotFound:
                    return NotFound();

                default:
                    break;
            }
            return BadRequest(createResult);
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
        public async Task<IActionResult> UpdateBranch(Guid id, Dto.Branch.UpdateBranchRequest request, CancellationToken cancellationToken)
        {

            var result = await _branchAppService.UpdateAsync(id, request.Data.Name, request.Data.BranchNo, request.Data.Code, request.Data.Address, request.Data.Contact, cancellationToken);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            switch (result.ErrorType)
            {
                case ErrorType.Success:
                    break;

                case ErrorType.Forbidden:
                    return Forbid();
                case ErrorType.NotFound:
                    return NotFound();
                default:
                    break;
            }

            return BadRequest(result);

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
            var result = await _branchAppService.DeleteAsync(id, cancellationToken);
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
    }
}
