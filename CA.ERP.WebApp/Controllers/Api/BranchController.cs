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
using Dto = CA.ERP.Shared.Dto;
using Dom = CA.ERP.Domain.BranchAgg;
using OneOf;
using OneOf.Types;
using Microsoft.AspNetCore.Authorization;
using CA.ERP.Shared.Dto;
using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using CA.ERP.Application.CommandQuery.BranchCommandQuery.GetManyBranch;
using CA.ERP.Application.CommandQuery.BranchCommandQuery.GetOneBranch;
using CA.ERP.Application.CommandQuery.BranchCommandQuery.CreateBranch;
using CA.ERP.Application.CommandQuery.BranchCommandQuery.UpdateBranch;
using CA.ERP.Application.CommandQuery.BranchCommandQuery.DeleteBranch;

namespace CA.ERP.WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BranchController: ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BranchController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        /// <summary>
        /// Get multiple branches
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dto.PaginatedResponse<Dto.Branch.BranchView>>> Get(CancellationToken cancellationToken)
        {
            var query = new GetManyBranchQuery();
            var paginatedBranches = await _mediator.Send(query, cancellationToken);

            var dtoBranches = _mapper.Map<List<Dto.Branch.BranchView>>(paginatedBranches.Data);
            var response = new Dto.PaginatedResponse<Dto.Branch.BranchView>() {
                Data = dtoBranches,
                TotalCount = paginatedBranches.TotalCount
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dto.Branch.BranchView>> Get(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetOneBranchQuery(id);

            DomainResult<Branch> result = await _mediator.Send(query, cancellationToken);
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
        public async Task<ActionResult<Dto.CreateResponse>> CreateBranch(Dto.Branch.CreateBranchRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateBranchCommand(request.Data.Name, request.Data.BranchNo, request.Data.Code, request.Data.Address, request.Data.Contact);

            DomainResult<Guid> createResult = await _mediator.Send(command, cancellationToken);
            if (createResult.IsSuccess)
            {
                var response = new Dto.CreateResponse()
                {
                    Id = createResult.Result
                };
                return Ok(response);
            }

            return HandleDomainResult(createResult);
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
            var command = new UpdateBranchCommand(id, request.Data.Name, request.Data.BranchNo, request.Data.Code, request.Data.Address, request.Data.Contact);

            DomainResult result = await _mediator.Send(command, cancellationToken);

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
            var command = new DeleteBranchCommand(id);

            DomainResult result = await _mediator.Send(command, cancellationToken);

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
