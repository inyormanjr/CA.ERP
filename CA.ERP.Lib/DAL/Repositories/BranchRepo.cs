using CA.ERP.Lib.DAL.IRepositories;
using CA.ERP.Lib.Domain.BranchAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Lib.DAL.Repositories
{
    public class BranchRepo : IBranchRepo
    {
        private readonly CADataContext context;

        public BranchRepo(CADataContext context)
        {
            this.context = context;
        }
        public void Delete( Branch entity)
        {
            this.context.Branches.Remove(entity);
        }

        public async Task<List<Branch>> GetAll()
        {
            var branches = await this.context.Branches.ToListAsync();
            return branches;
        }

        public async Task<List<Branch>> GetAll(int skip, int take)
        {
            var branches = await this.context.Branches.AsQueryable().Skip(skip).Take(take).ToListAsync();
            return branches;
        }

        public async Task<Branch> GetById(int id)
        {
            var branch = await this.context.Branches.FirstOrDefaultAsync(x => x.Id == id);
            if (branch == null) return null;
            return branch;
        }

        public  void Insert(Branch entity)
        {
            this.context.Add(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
    }
}
