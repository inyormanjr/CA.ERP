using CA.ERP.Domain.Base;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.Repository;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.BranchAgg
{
    public interface IBranchRepository : IRepository<Branch>
    {

        Task<List<Branch>> GetBranchsAsync(List<Guid> branchIds, CancellationToken cancellationToken);

    }
}
