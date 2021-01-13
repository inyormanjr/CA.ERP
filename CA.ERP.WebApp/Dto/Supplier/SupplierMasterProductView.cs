using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.Supplier
{
    public class SupplierMasterProductView
    {
        public Guid MasterProductId { get; set; }
        public Guid SupplierId { get; set; }
        public decimal CostPrice { get; set; }
    }
}
