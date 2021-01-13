using CA.ERP.Domain.Base;
using CA.ERP.Domain.Common;
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
        Task<OneOf<Success, None>> AddSupplierBrandAsync(Guid supplierId, SupplierBrand supplierBrand, CancellationToken cancellationToken);
        Task<OneOf<Success, None>> DeleteSupplierBrandAsync(Guid id, Guid brandId, CancellationToken cancellationToken);
        Task<List<SupplierBrandLite>> GetSupplierBrandsAsync(Guid supplierId, Status status = Status.Active, CancellationToken cancellationToken = default);
    }
}