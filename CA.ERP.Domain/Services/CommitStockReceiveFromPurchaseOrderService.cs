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

            foreach (var item in stockReceive.Items.Where(i => i.Status == ERP.Common.Types.StockReceiveItemStatus.Received))
            {
                var purchaseOrderItem = purchaseOrder.PurchaseOrderItems.FirstOrDefault(poi => poi.Id == item.PurchaseOrderItemId);
                var stockCreateResult = Stock.Create(item.MasterProductId, purchaseOrder.SupplierId, stockReceive.BranchId, item.StockNumber, item.SerialNumber, purchaseOrderItem.CostPrice, purchaseOrder.SupplierName, item.BrandName, item.Model, _dateTimeProvider);

                if (!stockCreateResult.IsSuccess)
                {
                    return stockCreateResult.ConvertTo<List<Stock>>();
                }

                item.StockId = stockCreateResult.Result.Id;

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
