using CA.ERP.Common.Types;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.MasterProductAgg
{
    public interface IMasterProductRepository : IRepository<MasterProduct>
    {
        Task<int> GetCountAsync(string model, Status status = Status.Active, CancellationToken cancellationToken = default);
        Task<List<MasterProduct>> GetManyAsync(string model, int skip = 0, int take = int.MaxValue, Status status = Status.Active, CancellationToken cancellationToken = default);
        Task<List<MasterProduct>> GetManyWithBrandAndSupplierAsync(Guid brandId, Guid supplierId, CancellationToken cancellationToken);
    }
}
