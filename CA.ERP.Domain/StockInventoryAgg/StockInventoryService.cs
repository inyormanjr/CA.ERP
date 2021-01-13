using CA.ERP.Domain.Base;
using CA.ERP.Domain.UnitOfWorkAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockInventoryAgg
{
    public class StockInventoryService : ServiceBase
    {
        private readonly IStockInventoryRepository _stockInventoryRepository;
        private readonly IStockInventoryFactory _stockInventoryFactory;

        public StockInventoryService(
            IUnitOfWork unitOfWork,
            IUserHelper userHelper,
            IStockInventoryRepository stockInventoryRepository,
            IStockInventoryFactory stockInventoryFactory) 
            : base(unitOfWork, userHelper)
        {
            _stockInventoryRepository = stockInventoryRepository;
            _stockInventoryFactory = stockInventoryFactory;
        }

        public async Task<StockInventory> GetStockInventoryAsync(Guid masterProductId, Guid branchId)
        {
            var stockInventoryOption = await _stockInventoryRepository.GetOneAsync(masterProductId, branchId);
            return stockInventoryOption.Match(
                f0: stockInventory => stockInventory,
                f1: _ => _stockInventoryFactory.Create(masterProductId, branchId)
                );
        }
    }
}
