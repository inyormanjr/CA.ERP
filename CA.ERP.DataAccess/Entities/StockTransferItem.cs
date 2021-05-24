using System;

namespace CA.ERP.DataAccess.Entities
{
    public class StockTransferItem : EntityBase
    {
        public Guid Id { get; set; }

        public Guid StockTransferId { get; set; }

        public Guid StockId { get; set; }

        public Stock Stock { get; set; }

        public StockTransfer StockTransfer { get; set; }

    }
}
