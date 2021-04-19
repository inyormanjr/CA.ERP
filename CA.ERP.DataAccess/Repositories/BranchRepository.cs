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
using CA.ERP.DataAccess.Repositories;
using CA.ERP.Domain.Core;
using CA.ERP.Common.Types;

namespace CA.ERP.Lib.DAL.Repositories
{
    public class BranchRepository : AbstractRepository<Branch, Dal.Branch>, IBranchRepository
    {
        public BranchRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override async Task<List<Branch>> GetManyAsync(int skip = 0, int take = int.MaxValue, Status status = Status.Active, CancellationToken cancellationToken = default)
        {
            IQueryable<Dal.Branch> queryable = generateQuery(status);

            return await queryable.OrderBy(b => b.Name).Select(e => _mapper.Map<Dal.Branch, Branch>(e)).ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<Branch>> GetBranchsAsync(List<Guid> branchIds, CancellationToken cancellationToken = default)
        {
            var branches = await _context.Branches.Where(b => branchIds.Contains(b.Id) && b.Status == Status.Active).OrderBy(b => b.Name).ToListAsync();
            return _mapper.Map<List<Branch>>(branches);
        }




        public new async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
            if (branch != null)
            {
                _context.Entry(branch).State = EntityState.Deleted;

            }
        }

        public async Task<Branch> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Branch ret = null;
            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

            if (branch != null)
            {
                ret = _mapper.Map<Branch>(branch);
            }
            return ret;
        }

        
    }
}
