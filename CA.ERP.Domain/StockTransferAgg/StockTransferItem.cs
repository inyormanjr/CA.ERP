using CA.ERP.Common.ErrorCodes;
using CA.ERP.Common.Types;
using CA.ERP.Domain.Core.DomainResullts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockTransferAgg
{
    public class StockTransferItem
    {
        public Guid Id { get; set; }

        public Guid StockTransferId { get; set; }

        public Guid StockId { get; set; }

        public string SupplierName { get; set; }

        public string BrandName { get; set; }

        public string Model { get; set; }

        public string StockNumber { get; set; }

        public string SerialNumber { get; set; }

        public StockStatus StockStatus { get; set; }

        public StockTransferItem()
        {

        }

        protected StockTransferItem(Guid id, Guid stockTransferId, Guid stockId)
        {
            Id = id;
            StockTransferId = stockTransferId;
            StockId = stockId;
        }

        public static DomainResult<StockTransferItem> Create(Guid stockTransferId, Guid stockId)
        {
            if (stockTransferId == Guid.Empty)
            {
                return DomainResult<StockTransferItem>.Error(StockTransferErrorCodes.InvalidStockTransferId, "Invalid stock transfer id");
            }

            if (stockId == Guid.Empty)
            {
                return DomainResult<StockTransferItem>.Error(StockTransferErrorCodes.InvalidStockId, "Invalid stock id");
            }

            return new StockTransferItem(Guid.NewGuid(), stockTransferId, stockId);
        }

    }
}
