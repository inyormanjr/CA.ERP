using AutoMapper;
using CA.ERP.DataAccess;
using CA.ERP.Domain.BranchAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA.ERP.Common.Extensions;
using OneOf;
using OneOf.Types;
using System.Threading;
using Dal = CA.ERP.DataAccess.Entities;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.Common;

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

        public async Task<Guid> AddAsync(Branch branch, CancellationToken cancellationToken)
        {
            branch.ThrowIfNullArgument(nameof(branch));
            var dalBranch = _mapper.Map<Dal.Branch>(branch);
            await _context.AddAsync(dalBranch, cancellationToken: cancellationToken);
            branch.Id = dalBranch.Id;
            return branch.Id;
        }

        public async Task<OneOf<Success, None>> DeleteAsync(Guid entityId, CancellationToken cancellationToken)
        {
            OneOf<Success, None> ret = default(None);
            var toDelete = await _context.Branches.FirstOrDefaultAsync(b => b.Id == entityId, cancellationToken: cancellationToken);
            if (toDelete != null)
            {
                _context.Entry(toDelete).State = EntityState.Deleted;
                await _context.SaveChangesAsync(cancellationToken: cancellationToken);
                ret = default(Success);
            }
            
            return ret;
        }

        public async Task<List<Branch>> GetAll(CancellationToken cancellationToken)
        {
            var branches = await this._context.Branches.ToListAsync(cancellationToken: cancellationToken);
            return _mapper.Map<List<Branch>>(branches);
        }

        public async Task<List<Branch>> GetAll(int skip, int take, CancellationToken cancellationToken)
        {
            var branches = await this._context.Branches.AsQueryable().Skip(skip).Take(take).ToListAsync(cancellationToken: cancellationToken);
            return _mapper.Map<List<Branch>>(branches);
        }




        public async Task<OneOf<Guid, None>> UpdateAsync(Guid id, Branch branch, CancellationToken cancellationToken = default)
        {
            OneOf<Guid, None> result = default(None);
            var dalBranch = await _context.Branches.FirstOrDefaultAsync<Dal.Branch>(b => b.Id == id, cancellationToken: cancellationToken);
            if (branch != null)
            {
                _mapper.Map(branch, dalBranch);
                dalBranch.Id = id;
                _context.Entry(dalBranch).State = EntityState.Modified;
                await _context.SaveChangesAsync(cancellationToken: cancellationToken);
                result = dalBranch.Id;
            }

            return result;
        }


        public async Task<OneOf<Branch, None>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
            if (branch == null) return null;
            return _mapper.Map<Branch>(branch);
        }

        public async Task<List<Branch>> GetBranchsAsync(List<Guid> branchIds, CancellationToken cancellationToken = default)
        {
            var branches = await _context.Branches.Where(b => branchIds.Contains(b.Id)).ToListAsync();
            return _mapper.Map<List<Branch>>(branches);
        }


       
        public Task<List<Branch>> GetAll(int skip = 0, int take = 0, Status status = Status.Active, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
