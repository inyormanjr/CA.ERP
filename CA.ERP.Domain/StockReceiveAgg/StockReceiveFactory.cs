using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockReceiveAgg
{
    public class StockReceiveFactory : IStockReceiveFactory
    {
        private readonly IUserHelper _userHelper;

        public StockReceiveFactory(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }
        public StockReceive CreateStockReceive(Guid? purchaseOrderId, Guid branchId, StockSource stockSource, Guid supplierId, string deliveryReference, List<Stock> stocks)
        {
            foreach (var stock in stocks)
            {
                stock.BranchId = branchId;
            }

            var stockReceive = new StockReceive() { 
                BranchId = branchId,
                PurchaseOrderId = purchaseOrderId,
                Status = Common.Status.Active,
                StockSouce = stockSource,
                SupplierId = supplierId,
                CreatedBy = _userHelper.GetCurrentUserId(),
                UpdatedBy = _userHelper.GetCurrentUserId(),
                DeliveryReference = deliveryReference,
                Stocks = stocks
            };

            return stockReceive;
        }
    }
}
