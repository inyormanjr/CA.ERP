
using CA.ERP.RdlcDesign.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockAgg
{
    public class Stock:ModelBase
    {
        public Stock()
        {
            StockStatus = StockStatus.Available;
        }

        public Guid MasterProductId { get; set; }
        public Guid StockReceiveId { get; set; }
        public Guid? PurchaseOrderItemId { get; set; }
        public string StockNumber { get; set; }
        public string SerialNumber { get; set; }
        public StockStatus StockStatus { get; set; }
        public decimal CostPrice { get; set; }

        public string BrandName { get; set; }
        public string Model { get; set; }

    }

    public enum StockStatus : int
    {
        Available = 1,
        Free = 2,
        Sold = 3
    }
}
