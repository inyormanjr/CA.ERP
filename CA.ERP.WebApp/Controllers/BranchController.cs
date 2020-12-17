using AutoMapper;
using CA.ERP.Domain.BranchAgg;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers
{
    public class BranchController:BaseApiController
    {
        private readonly IMapper _mapper;

        public BranchController(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public IBranchRepository _branchRepository { get; }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var branches = await this._branchRepository.GetAll();
            return Ok(branches);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBranch(Branch branch)
        {
            this._branchRepository.Insert(branch);
            await this._branchRepository.SaveAll();
            return Ok(branch);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(int id, Branch branch)
        {
            var branchFromRepo = await this._branchRepository.GetById(id);
            if (branchFromRepo == null) return BadRequest("No Branch Found");
            this._mapper.Map(branch, branchFromRepo);
            if (await this._branchRepository.SaveAll())
                return NoContent();
            throw new System.Exception($"Updating data {id} failed");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            var branchFromRepo = await this._branchRepository.GetById(id);
            if (branchFromRepo == null) return BadRequest("No Branch Found");
            this._branchRepository.Delete(branchFromRepo.Id);
            if(await this._branchRepository.SaveAll()) 
                return Ok("Branch deleted.");
            throw new System.Exception($"Updating data {id} failed");
        }
    }
}
