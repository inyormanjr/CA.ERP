using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class MasterProduct : EntityBase
    {

        public string Model { get; set; }
        public string Description { get; set; }
        public int ProductStatus { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        public List<SupplierMasterProduct> SupplierMasterProducts { get; set; } = new List<SupplierMasterProduct>();
        public List<StockReceiveItem> StockReceiveItems { get; set; } = new List<StockReceiveItem>();
        public List<Stock> Stocks { get;  set; }
        //public List<StockInventory> StockInventories { get; set; }
    }
}
