using CA.ERP.Common.ErrorCodes;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.StockCounterAgg;
using CA.ERP.Domain.StockReceiveAgg;
using CA.ERP.Domain.StockTransferAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CA.ERP.Domain.Services
{
    public interface IStockReceiveGeneratorFromStockTransferService
    {
        DomainResult<StockReceive> Generate(StockTransfer stockTransfer, StockCounter stockCounter);
    }

    public class StockReceiveGeneratorFromStockTransferService : IStockReceiveGeneratorFromStockTransferService
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IStockNumberService _stockNumberService;

        public StockReceiveGeneratorFromStockTransferService(IDateTimeProvider dateTimeProvider, IStockNumberService stockNumberService)
        {
            _dateTimeProvider = dateTimeProvider;
            _stockNumberService = stockNumberService;
        }

        public DomainResult<StockReceive> Generate(StockTransfer stockTransfer, StockCounter stockCounter)
        {
            if (stockTransfer.StockTransferStatus != ERP.Common.Types.StockTransferStatus.Pending)
            {
                return DomainResult<StockReceive>.Error(StockReceiveErrorCodes.StockTransferNotPending, $"Stock transfer should have a status of Pending. Current status is {stockTransfer.StockTransferStatus}");
            }

            var createResult = StockReceive.CreateForStockTranfer(stockTransfer.Id, stockTransfer.DestinationBranchId, _dateTimeProvider);

            if (!createResult.IsSuccess)
            {
                return createResult;
            }

            var stockReceive = createResult.Result;
            var stockNumbers = _stockNumberService.GenerateStockNumbers(stockCounter);
            foreach (var item in stockTransfer.Items)
            {
                for (int i = 0; i < item.RequestedQuantity; i++)
                {

                    var stockReceiveItemResult = StockReceiveItem.CreateForStockTransfer(item.MasterProductId, stockReceive.Id, item.Id, stockReceive.BranchId, stockNumbers.Take(1).FirstOrDefault(), item.BrandName, item.Model);
                    if (!stockReceiveItemResult.IsSuccess)
                    {
                        return stockReceiveItemResult.ConvertTo<StockReceive>();
                    }
                    stockReceive.AddItem(stockReceiveItemResult.Result);
                }
            }
            stockTransfer.Generated();

            return createResult;
        }
    }
}
