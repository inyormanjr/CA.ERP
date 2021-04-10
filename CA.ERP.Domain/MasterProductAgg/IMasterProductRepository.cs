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
        Task<List<MasterProduct>> GetManyWithBrandAndSupplierAsync(Guid brandId, Guid supplierId, CancellationToken cancellationToken);
    }
}
