using System;

namespace CA.ERP.DataAccess.Entities
{
    public class StockTransferItem : EntityBase
    {
        public Guid Id { get; set; }

        public Guid StockTransferId { get; set; }

        public Guid MasterProductId { get; set; }

        public int RequestedQuantity { get; set; }

        public MasterProduct MasterProduct { get; set; }

        public StockTransfer StockTransfer { get; set; }

    }
}
