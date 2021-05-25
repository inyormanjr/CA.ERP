using CA.ERP.Common.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Shared.Dto.StockTransfer
{
    public class StockTransferItemCreate
    {
        public Guid MasterProductId { get; set; }

        public int RequestedQuantity { get; set; }

        public string BrandName { get; set; }

        public string Model { get; set; }
    }
}
