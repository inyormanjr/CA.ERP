using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Services
{
    public interface IStockReceiveGeneratorService
    {
        DomainResult<StockReceive> FromPurchaseOrder(PurchaseOrder purchaseOrder);
    }
    public class StockReceiveGeneratorService : IStockReceiveGeneratorService
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public StockReceiveGeneratorService(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }
        public DomainResult<StockReceive> FromPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            var createStockReceiveResult = StockReceive.Create(purchaseOrder.Id, purchaseOrder.Id, ERP.Common.Types.StockSource.PurchaseOrder, purchaseOrder.SupplierId, _dateTimeProvider);
            if (!createStockReceiveResult.IsSuccess)
            {
                return createStockReceiveResult;
            }

            var stockReceive = createStockReceiveResult.Result;
            foreach (var purchaseOrderItem in purchaseOrder.PurchaseOrderItems)
            {
                var stockReceiveItemResult = StockReceiveItem.Create(purchaseOrderItem.MasterProductId, stockReceive.Id, purchaseOrderItem.Id, purchaseOrder.Id, purchaseOrderItem.BrandName, purchaseOrderItem.Model);
                if (!stockReceiveItemResult.IsSuccess)
                {
                    return stockReceiveItemResult.ConvertTo<StockReceive>();
                }
                stockReceive.AddItem(stockReceiveItemResult.Result);
            }

            return createStockReceiveResult;
        }
    }
}
