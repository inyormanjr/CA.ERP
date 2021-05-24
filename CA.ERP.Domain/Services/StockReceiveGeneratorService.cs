using CA.ERP.Common.ErrorCodes;
using CA.ERP.Common.Types;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.StockCounterAgg;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CA.ERP.Domain.Services
{
    public interface IStockReceiveGeneratorService
    {
        DomainResult<StockReceive> FromPurchaseOrder(PurchaseOrder purchaseOrder, StockCounter stockCounter);
    }
    public class StockReceiveGeneratorService : IStockReceiveGeneratorService
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IStockNumberService _stockNumberService;

        public StockReceiveGeneratorService(IDateTimeProvider dateTimeProvider, IStockNumberService stockNumberService)
        {
            _dateTimeProvider = dateTimeProvider;
            _stockNumberService = stockNumberService;
        }
        public DomainResult<StockReceive> FromPurchaseOrder(PurchaseOrder purchaseOrder, StockCounter stockCounter)
        {
            if (purchaseOrder.PurchaseOrderStatus != PurchaseOrderStatus.Pending)
            {
                return DomainResult<StockReceive>.Error(StockReceiveErrorCodes.PurchaseOrderNotPending, $"Purchase Order should have a status of Pending. Current status is {purchaseOrder.PurchaseOrderStatus}");
            }

            var createStockReceiveResult = StockReceive.CreateForPurchaseOrder(purchaseOrder.DestinationBranchId, ERP.Common.Types.StockSource.PurchaseOrder, purchaseOrder.SupplierId, _dateTimeProvider, purchaseOrder.Id);
            if (!createStockReceiveResult.IsSuccess)
            {
                return createStockReceiveResult;
            }

            var stockReceive = createStockReceiveResult.Result;
            var stockNumbers = _stockNumberService.GenerateStockNumbers(stockCounter);
            foreach (var purchaseOrderItem in purchaseOrder.PurchaseOrderItems)
            {
                for (int i = 0; i < purchaseOrderItem.OrderedQuantity; i++)
                {

                    var stockReceiveItemResult = StockReceiveItem.Create(purchaseOrderItem.MasterProductId, stockReceive.Id, purchaseOrderItem.Id, purchaseOrder.DestinationBranchId, purchaseOrderItem.CostPrice, stockNumbers.Take(1).FirstOrDefault(), purchaseOrderItem.BrandName, purchaseOrderItem.Model);
                    if (!stockReceiveItemResult.IsSuccess)
                    {
                        return stockReceiveItemResult.ConvertTo<StockReceive>();
                    }
                    stockReceive.AddItem(stockReceiveItemResult.Result);
                }

                for (int i = 0; i < purchaseOrderItem.FreeQuantity; i++)
                {

                    var stockReceiveItemResult = StockReceiveItem.Create(purchaseOrderItem.MasterProductId, stockReceive.Id, purchaseOrderItem.Id, purchaseOrder.DestinationBranchId, 0, stockNumbers.Take(1).FirstOrDefault(), purchaseOrderItem.BrandName, purchaseOrderItem.Model);
                    if (!stockReceiveItemResult.IsSuccess)
                    {
                        return stockReceiveItemResult.ConvertTo<StockReceive>();
                    }
                    stockReceive.AddItem(stockReceiveItemResult.Result);
                }
            }
            purchaseOrder.Generated();
            return createStockReceiveResult;
        }
    }
}
