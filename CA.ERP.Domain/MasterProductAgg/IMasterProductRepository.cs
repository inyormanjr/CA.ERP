using CA.ERP.Domain.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.MasterProductAgg
{
    public interface IMasterProductRepository : IRepository<MasterProduct>
    {
        Task<MasterProduct> GetByIdAsync(Guid guid, CancellationToken cancellationToken);
    }
}
