using CA.ERP.Domain.Base;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.Core.Entity;
using CA.ERP.Domain.MasterProductAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.SupplierAgg
{
    public class SupplierMasterProduct : IValueObject
    {
        public Guid MasterProductId { get; private set; }
        public Guid SupplierId { get; private set; }
        public decimal CostPrice { get; private set; }

        protected SupplierMasterProduct(Guid masterProductId, Guid supplierId, decimal costPrice)
        {
            MasterProductId = masterProductId;
            SupplierId = supplierId;
            CostPrice = costPrice;
        }

        public static DomainResult<SupplierMasterProduct> Create(Guid masterProductId, Guid supplierId, decimal costPrice)
        {
            return DomainResult<SupplierMasterProduct>.Success(new SupplierMasterProduct(masterProductId, supplierId, costPrice));
        }

    }
}
