using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.SupplierAgg
{
    public class SupplierFactory : ISupplierFactory
    {
        public Supplier CreateSupplier(string name, string address, string contact, List<SupplierBrand> supplierBrands)
        {
            return new Supplier() {
                Name = name,
                Address = address,
                ContactPerson = contact,
                SupplierBrands = supplierBrands
            };
        }

    }
}
