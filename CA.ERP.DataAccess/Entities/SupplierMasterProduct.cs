using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class SupplierMasterProduct : EntityBase
    {
        public Guid MasterProductId { get; set; }
        public Guid SupplierId { get; set; }
        public decimal CostPrice { get; set; }

        public MasterProduct MasterProduct { get; set; }
        //public Supplier Supplier { get; set; }
    }
}
