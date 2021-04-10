using CA.ERP.Domain.Base;
using CA.ERP.Domain.Common;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.Core.Repository;
using FluentValidation.Results;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.SupplierAgg
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<List<Supplier>> GetManySupplierAsync(string name, int skip, int take, CancellationToken cancellationToken);
        Task<int> GetCountSupplierAsync(string name, CancellationToken cancellationToken);
        Task AddSupplierBrandAsync(Guid supplierId, SupplierBrand supplierBrand, CancellationToken cancellationToken);
        Task<List<SupplierBrand>> GetManySupplierBrandAsync(Guid supplierId, CancellationToken cancellationToken);
        Task DeleteSupplierBrandAsync(Guid id, Guid brandId, CancellationToken cancellationToken);

        Task AddOrUpdateAsync(SupplierMasterProduct supplierMasterProduct, CancellationToken cancellationToken);

        Task AddOrUpdateSupplierMasterProductCostPriceAsync(Guid supplierId, Guid masterProductId, decimal costPrice , CancellationToken cancellationToken = default);
    }
}
