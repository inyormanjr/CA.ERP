﻿using AutoMapper;
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

namespace CA.ERP.WebApp.Controllers.Api
{
    [Authorize]
    public class BranchController:BaseApiController
    {
        private readonly BranchService _branchService;

        public BranchController(IServiceProvider serviceProvider, BranchService branchService)
            : base(serviceProvider)
        {
            _branchService = branchService;
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
            var branches = await _branchService.GetManyAsync();
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
            var branchOption = await _branchService.GetOneAsync(id, cancellationToken);
            return branchOption.Match<ActionResult>(
                f0: brand =>
                {
                    return Ok(_mapper.Map<Dto.Branch.BranchView>(brand));
                },
                f1: notfound => NotFound()
            );
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

            var createResult = await _branchService.CreateBranchAsync(request.Data.Name, request.Data.BranchNo, request.Data.Code, request.Data.Address, request.Data.Contact, cancellationToken);

            return createResult.Match<ActionResult>(
                f0: (id) =>
                {
                    var response = new Dto.CreateResponse()
                    {
                        Id = id
                    };
                    return Ok(response);
                },
                f1: (validationErrors) => {
                    var error = new ErrorResponse(HttpContext.TraceIdentifier) { 
                        GeneralError = "Validation Error", 
                        ValidationErrors = _mapper.Map<List<ValidationError>>(validationErrors) 
                    };
                    return BadRequest(error); 
                }
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
        public async Task<IActionResult> UpdateBranch(Guid id, Dto.Branch.UpdateBranchRequest request, CancellationToken cancellationToken)
        {
            var domBranch = _mapper.Map<Dom.Branch>(request.Data);
            var result = await _branchService.UpdateAsync(id, domBranch, cancellationToken);

            return result.Match<IActionResult>(
                f0: (branch) => NoContent(),
                f1: (validationErrors) => {
                    var error = new ErrorResponse(HttpContext.TraceIdentifier)
                    {
                        GeneralError = "Validation Error",
                        ValidationErrors = _mapper.Map<List<ValidationError>>(validationErrors)
                    };
                    return BadRequest(error);
                },
                f2: (error) => NotFound()
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