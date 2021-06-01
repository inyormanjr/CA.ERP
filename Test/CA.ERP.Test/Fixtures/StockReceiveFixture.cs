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

            var createStockReceiveResult = StockReceive.CreateForPurchaseOrder(purchaseOrder.Id, purchaseOrder.DestinationBranchId, dateTimeProvider);
            if (!createStockReceiveResult.IsSuccess)
            {
                throw new Exception("Create Stock receive null");
            }

            var stockReceive = createStockReceiveResult.Result;

            foreach (var purchaseOrderItem in purchaseOrder.PurchaseOrderItems)
            {
                for (int i = 0; i < purchaseOrderItem.OrderedQuantity; i++)
                {

                    var stockReceiveItemResult = StockReceiveItem.CreateForPurchaseOrder(purchaseOrderItem.MasterProductId, stockReceive.Id, purchaseOrderItem.Id, purchaseOrder.DestinationBranchId, purchaseOrderItem.CostPrice, stockNumberCount++.ToString(), purchaseOrderItem.BrandName, purchaseOrderItem.Model);
                    if (!stockReceiveItemResult.IsSuccess)
                    {
                        throw new Exception("Stock receive item creation error");
                    }
                    stockReceive.AddItem(stockReceiveItemResult.Result);
                }


                for (int i = 0; i < purchaseOrderItem.FreeQuantity; i++)
                {

                    var stockReceiveItemResult = StockReceiveItem.CreateForPurchaseOrder(purchaseOrderItem.MasterProductId, stockReceive.Id, purchaseOrderItem.Id, purchaseOrder.DestinationBranchId, 0, stockNumberCount++.ToString(), purchaseOrderItem.BrandName, purchaseOrderItem.Model);
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
