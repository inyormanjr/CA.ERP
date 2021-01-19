using CA.ERP.Domain.Base;
using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.StockMoveAgg;
using CA.ERP.Domain.UnitOfWorkAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockInventoryAgg
{
    public class StockInventoryService : ServiceBase
    {
        private readonly IStockReceiveStockMoveGenerator _stockReceiveStockMoveGenerator;
        private readonly IStockInventoryRepository _stockInventoryRepository;
        private readonly IStockMoveRepository _stockMoveRepository;
        private readonly IStockInventoryFactory _stockInventoryFactory;

        public StockInventoryService(
            IUnitOfWork unitOfWork,
            IUserHelper userHelper,
            IStockReceiveStockMoveGenerator stockReceiveStockMoveGenerator,
            IStockInventoryRepository stockInventoryRepository,
            IStockMoveRepository stockMoveRepository,
            IStockInventoryFactory stockInventoryFactory) 
            : base(unitOfWork, userHelper)
        {
            _stockReceiveStockMoveGenerator = stockReceiveStockMoveGenerator;
            _stockInventoryRepository = stockInventoryRepository;
            _stockMoveRepository = stockMoveRepository;
            _stockInventoryFactory = stockInventoryFactory;
        }

        public async Task<StockInventory> GetStockInventoryAsync(Guid masterProductId, Guid branchId)
        {
            var stockInventoryOption = await _stockInventoryRepository.GetOneNoTrackingAsync(masterProductId, branchId);
            return stockInventoryOption.Match(
                f0: stockInventory => stockInventory,
                f1: _ => _stockInventoryFactory.Create(masterProductId, branchId)
                );
        }

        public async Task CalculateStockInventoryForStockReceive(Guid stockReceiveId, Stock stock, CancellationToken cancellationToken)
        {
            var stockInventory = await GetStockInventoryAsync(stock.MasterProductId, stock.BranchId);
            var prevStockMoveOption = await _stockMoveRepository.GetLatestStockMoveAsync(stock.MasterProductId, stock.BranchId, cancellationToken);
            StockMove prevStockMove = prevStockMoveOption.Match(f0: sm => sm, f1: _ => null);
            var newStockMove = _stockReceiveStockMoveGenerator.Generate(stockReceiveId, prevStockMove, stock);

            stockInventory.Quantity += newStockMove.ChangeQuantity;

            await _stockInventoryRepository.AddOrUpdateAsync(stockInventory);
            await _stockMoveRepository.AddStockMoveAsync(newStockMove, cancellationToken);
        }
    }
}
