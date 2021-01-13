using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.SupplierAgg
{
    public interface ISupplierFactory : IFactory<Supplier>
    {
        Supplier CreateSupplier(string name, string address, string contact, List<SupplierBrand> supplierBrands);
    }
}
