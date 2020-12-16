using AutoMapper;
using CA.ERP.Lib.DAL.IRepositories;
using CA.ERP.Lib.Domain.BranchAgg;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers
{
    public class BranchController:BaseApiController
    {
        private readonly IMapper mapper;

        public BranchController(IBranchRepo repo, IMapper mapper)
        {
            Repo = repo;
            this.mapper = mapper;
        }

        public IBranchRepo Repo { get; }

        [HttpGet("GetBranches")]
        public async Task<IActionResult> Get()
        {
            var branches = await this.Repo.GetAll();
            return Ok(branches);
        }

        [HttpPost("CreateBranch")]
        public async Task<IActionResult> CreateBranch(Branch branch)
        {
            this.Repo.Insert(branch);
            await this.Repo.SaveAll();
            return Ok(branch);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(int id, Branch branch)
        {
            var branchFromRepo = await this.Repo.GetById(id);
            if (branchFromRepo == null) return BadRequest("No Branch Found");
            this.mapper.Map(branch, branchFromRepo);
            if (await this.Repo.SaveAll())
                return NoContent();
            throw new System.Exception($"Updating data {id} failed");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            var branchFromRepo = await this.Repo.GetById(id);
            if (branchFromRepo == null) return BadRequest("No Branch Found");
            this.Repo.Delete(branchFromRepo);
            if(await this.Repo.SaveAll()) return Ok("Branch deleted.");
            throw new System.Exception($"Updating data {id} failed");
        }
    }
}
