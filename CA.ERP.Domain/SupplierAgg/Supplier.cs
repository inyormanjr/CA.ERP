using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.SupplierAgg
{
    public class Supplier: ModelBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }

        public List<SupplierBrand> SupllierBrands { get; set; } = new List<SupplierBrand>();
    }
}
