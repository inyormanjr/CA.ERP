using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Shared.Dto.StockReceive
{
    public class StockReceiveCommit
    {
        public Guid Id { get; set; }


        public string DeliveryReference { get; set; }
        public List<StockReceiveItemCommit> Items { get; set; } = new List<StockReceiveItemCommit>();
    }
}
