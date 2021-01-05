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

namespace CA.ERP.Lib.DAL.Repositories
{
    public class BranchRepository : AbstractRepository<Branch, Dal.Branch>, IBranchRepository
    {
        public BranchRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<List<Branch>> GetBranchsAsync(List<Guid> branchIds, CancellationToken cancellationToken = default)
        {
            var branches = await _context.Branches.Where(b => branchIds.Contains(b.Id) && b.Status == Status.Active).ToListAsync();
            return _mapper.Map<List<Branch>>(branches);
        }


       
        
    }
}
