using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Shared.Dto.StockTransfer
{
    public class StockTransferItemView
    {
        public Guid Id { get; set; }

        public Guid StockTransferId { get; set; }

        public Guid MasterProductId { get; set; }

        public int RequestedQuantity { get; set; }

        public string BrandName { get; set; }

        public string Model { get; set; }
    }
}
