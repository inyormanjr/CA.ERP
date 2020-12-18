using AutoMapper;
using CA.ERP.DataAccess;
using CA.ERP.Domain.BranchAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Lib.DAL.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly CADataContext _context;
        private readonly IMapper _mapper;

        public BranchRepository(CADataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Delete(string entityId)
        {
            var toDelete = _context.Branches.FirstOrDefault(b => b.Id == entityId);
            if (toDelete != null)
            {
                _context.Entry(toDelete).State = EntityState.Deleted;
            }
        }

        public async Task<List<Branch>> GetAll()
        {
            var branches = await this._context.Branches.ToListAsync();
            return _mapper.Map<List<Branch>>(branches);
        }

        public async Task<List<Branch>> GetAll(int skip, int take)
        {
            var branches = await this._context.Branches.AsQueryable().Skip(skip).Take(take).ToListAsync();
            return _mapper.Map<List<Branch>>(branches);
        }

        public async Task<Branch> GetById(string id)
        {
            var branch = await this._context.Branches.FirstOrDefaultAsync(x => x.Id == id);
            if (branch == null) return null;
            return _mapper.Map<Branch>(branch);
        }

        public  void Insert(Branch entity)
        {
            _context.Add(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
