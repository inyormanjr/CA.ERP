using CA.ERP.Common.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Shared.Dto.StockTransfer
{
    public class StockTransferItemCreate
    {
        public Guid StockId { get; set; }

        public string SupplierName { get; set; }

        public string BrandName { get; set; }

        public string Model { get; set; }

        public string StockNumber { get; set; }

        public string SerialNumber { get; set; }

        public StockStatus StockStatus { get; set; }
    }
}
