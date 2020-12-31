﻿using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.SupplierAgg
{
    public interface ISupplierMasterProductRepository : IRepository<SupplierMasterProduct>
    {
        Task AddOrUpdateAsync(SupplierMasterProduct supplierMasterProduct, CancellationToken cancellationToken);
    }
}
