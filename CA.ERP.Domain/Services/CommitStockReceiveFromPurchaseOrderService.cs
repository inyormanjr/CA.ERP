using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CA.ERP.Domain.Services
{
    public interface ICommitStockReceiveFromPurchaseOrderService
    {
        DomainResult<List<Stock>> Commit(PurchaseOrder purchaseOrder, StockReceive stockReceive);
    }
    public class CommitStockReceiveFromPurchaseOrderService : ICommitStockReceiveFromPurchaseOrderService
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public CommitStockReceiveFromPurchaseOrderService(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }
        public DomainResult<List<Stock>> Commit(PurchaseOrder purchaseOrder, StockReceive stockReceive)
        {
            if (purchaseOrder == null)
            {
                throw new ArgumentException(nameof(purchaseOrder));
            }

            if (stockReceive == null)
            {
                throw new ArgumentException(nameof(stockReceive));
            }
            List<Stock> stocks = new List<Stock>();
            foreach (var item in stockReceive.Items)
            {
                var purchaseOrderItem = purchaseOrder.PurchaseOrderItems.FirstOrDefault(poi => poi.Id == item.PurchaseOrderItemId);
                var stockCreateResult = Stock.Create(item.MasterProductId, stockReceive.Id, purchaseOrderItem.Id, stockReceive.BranchId, item.StockNumber, item.SerialNumber, item.StockStatus, purchaseOrderItem.CostPrice, item.BrandName, item.Model );
                if (!stockCreateResult.IsSuccess)
                {
                    return stockCreateResult.ConvertTo<List<Stock>>();
                }

                stocks.Add(stockCreateResult.Result);
                DomainResult changeDeliveredQuantityResult = purchaseOrderItem.ChangeDeliveredQuantity(purchaseOrderItem.DeliveredQuantity + 1);

                if (!changeDeliveredQuantityResult.IsSuccess)
                {
                    return DomainResult<List<Stock>>.Error(changeDeliveredQuantityResult.ErrorCode, changeDeliveredQuantityResult.ErrorMessage);
                }
            }

            stockReceive.Commit(_dateTimeProvider);
            
            return DomainResult<List<Stock>>.Success(stocks);
        }
    }
}
