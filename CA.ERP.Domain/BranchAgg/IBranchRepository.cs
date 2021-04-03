using CA.ERP.Domain.Base;
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
        Task<Branch> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Branch>> GetBranchsAsync(List<Guid> branchIds, CancellationToken cancellationToken);
    }
}
