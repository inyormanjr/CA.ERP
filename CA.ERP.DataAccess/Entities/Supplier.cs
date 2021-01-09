using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class Supplier : EntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }

        public List<SupplierBrand> SupplierBrands { get; set; } = new List<SupplierBrand>();
        public List<SupplierMasterProduct> SupplierMasterProducts { get; set; } = new List<SupplierMasterProduct>();
        public List<PurchaseOrder> PurchaseOrders { get;  set; }
        public List<StockReceive> StockReceives { get; set; }
    }
}
