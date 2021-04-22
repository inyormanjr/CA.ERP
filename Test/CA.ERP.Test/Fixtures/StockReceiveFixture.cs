using CA.ERP.Domain.Core;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.StockReceiveAgg;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Test.Fixtures
{
    public class StockReceiveFixture
    {
        public StockReceive GetStockReceive(PurchaseOrder purchaseOrder, IDateTimeProvider dateTimeProvider)
        {
            int stockNumberCount = 0;

            var createStockReceiveResult = StockReceive.Create(purchaseOrder.Id, purchaseOrder.DestinationBranchId, ERP.Common.Types.StockSource.PurchaseOrder, purchaseOrder.SupplierId, dateTimeProvider);
            if (!createStockReceiveResult.IsSuccess)
            {
                throw new Exception("Create Stock receive null");
            }

            var stockReceive = createStockReceiveResult.Result;

            foreach (var purchaseOrderItem in purchaseOrder.PurchaseOrderItems)
            {
                for (int i = 0; i < purchaseOrderItem.TotalQuantity; i++)
                {

                    var stockReceiveItemResult = StockReceiveItem.Create(purchaseOrderItem.MasterProductId, stockReceive.Id, purchaseOrderItem.Id, purchaseOrder.DestinationBranchId, stockNumberCount++.ToString(), purchaseOrderItem.BrandName, purchaseOrderItem.Model);
                    if (!stockReceiveItemResult.IsSuccess)
                    {
                        throw new Exception("Stock receive item creation error");
                    }
                    stockReceive.AddItem(stockReceiveItemResult.Result);
                }
            }

            return stockReceive;
        }
    }
}
