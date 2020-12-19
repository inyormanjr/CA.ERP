using AutoMapper;
using CA.ERP.Domain.BranchAgg;
using DTO =  CA.ERP.WebApp.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> CreateBranch(Branch branch)
        {
            this._branchRepository.Insert(branch);
            await this._branchRepository.SaveAll();
            return Ok(branch);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(string id, Branch branch)
        {
            var branchFromRepo = await this._branchRepository.GetById(id);
            if (branchFromRepo == null) return BadRequest("No Branch Found");
            this._mapper.Map(branch, branchFromRepo);
            if (await this._branchRepository.SaveAll())
                return NoContent();
            throw new System.Exception($"Updating data {id} failed");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(string id)
        {
            _branchRepository.Delete(id);
            if(await this._branchRepository.SaveAll()) 
                return Ok("Branch deleted.");
            throw new System.Exception($"Updating data {id} failed");
        }
    }
}
