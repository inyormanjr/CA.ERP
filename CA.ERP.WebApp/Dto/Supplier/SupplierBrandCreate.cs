using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.Supplier
{
    public class SupplierBrandCreate
    {
        public Guid SupplierId { get; set; }
        public Guid BrandId { get; set; }
    }
}
