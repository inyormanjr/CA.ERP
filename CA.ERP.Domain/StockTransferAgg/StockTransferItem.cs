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

        public Guid MasterProductId { get; set; }

        public int RequestedQuantity { get; set; }

        public string BrandName { get; set; }

        public string Model { get; set; }

        public StockTransferItem()
        {

        }

        protected StockTransferItem(Guid id, Guid stockTransferId, Guid masterProductId, int requestedQuantity)
        {
            Id = id;
            StockTransferId = stockTransferId;
            MasterProductId = masterProductId;
            RequestedQuantity = requestedQuantity;
        }

        public static DomainResult<StockTransferItem> Create(Guid stockTransferId, Guid masterProductId, int requestedQuantity)
        {
            if (stockTransferId == Guid.Empty)
            {
                return DomainResult<StockTransferItem>.Error(StockTransferErrorCodes.InvalidStockTransferId, "Invalid stock transfer id");
            }

            if (masterProductId == Guid.Empty)
            {
                return DomainResult<StockTransferItem>.Error(StockTransferErrorCodes.InvalidMasterProductId, "Invalid master product id");
            }

            if (requestedQuantity <= 0)
            {
                return DomainResult<StockTransferItem>.Error(StockTransferErrorCodes.RequestedQuantityMustGreaterThanZero, "Requested quantity must be greater than zero");
            }

            return new StockTransferItem(Guid.NewGuid(), stockTransferId, masterProductId, requestedQuantity);
        }

    }
}
