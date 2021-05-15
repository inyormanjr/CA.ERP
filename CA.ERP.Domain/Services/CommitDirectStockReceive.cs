using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CA.ERP.Domain.Services
{
    public interface ICommitDirectStockReceive
    {
        DomainResult<List<Stock>> Commit(StockReceive stockReceive);
    }
    public class CommitDirectStockReceive : ICommitDirectStockReceive
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public CommitDirectStockReceive(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public DomainResult<List<Stock>> Commit(StockReceive stockReceive)
        {
            if (stockReceive == null)
            {
                throw new ArgumentException(nameof(stockReceive));
            }

            List<Stock> stocks = new List<Stock>();
            foreach (var item in stockReceive.Items)
            {
                item.Commit(ERP.Common.Types.StockReceiveItemStatus.Received);
                var stockCreateResult = Stock.Create(item.MasterProductId, stockReceive.Id, null, stockReceive.BranchId, item.StockNumber, item.SerialNumber, item.CostPrice, item.BrandName, item.Model);
                if (!stockCreateResult.IsSuccess)
                {
                    return stockCreateResult.ConvertTo<List<Stock>>();
                }

                stocks.Add(stockCreateResult.Result);

            }

            stockReceive.Commit(_dateTimeProvider);

            return DomainResult<List<Stock>>.Success(stocks);
        }
    }
}
