﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto.Supplier
{
    public class SupplierMasterProductUpdate
    {
        public Guid MasterProductId { get; set; }
        public decimal CostPrice { get; set; }
    }
}
