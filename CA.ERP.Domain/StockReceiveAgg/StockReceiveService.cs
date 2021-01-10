using CA.ERP.Domain.Base;
using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.StockInventoryAgg;
using CA.ERP.Domain.UnitOfWorkAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using FluentValidation.Results;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockReceiveAgg
{
    public class StockReceiveService : ServiceBase<StockReceive>
    {
        private readonly IStockInventoryRepository _stockInventoryRepository;
        private readonly IStockReceiveFactory _stockReceiveFactory;
        private readonly IStockInventoryStockReceiveCalculator _stockInventoryStockReceiveCalculator;

        public StockReceiveService(
            IUnitOfWork unitOfWork,
            IRepository<StockReceive> repository,
            IStockInventoryRepository stockInventoryRepository,
            IValidator<StockReceive> validator,
            IUserHelper userHelper,
            IStockReceiveFactory stockReceiveFactory,
            IStockInventoryStockReceiveCalculator stockInventoryStockReceiveCalculator) 
            : base(unitOfWork, repository, validator, userHelper)
        {
            _stockInventoryRepository = stockInventoryRepository;
            _stockReceiveFactory = stockReceiveFactory;
            _stockInventoryStockReceiveCalculator = stockInventoryStockReceiveCalculator;
        }

        public async Task<OneOf<Guid, List<ValidationFailure>>> CreateStockReceive(Guid? purchaseOrderId, Guid branchId, StockSource stockSource, Guid supplierId, List<Stock> stocks, CancellationToken cancellationToken)
        {
            OneOf<Guid, List<ValidationFailure>> ret;
            StockReceive stockReceive = _stockReceiveFactory.CreateStockReceive(purchaseOrderId, branchId, stockSource, supplierId, stocks);
            
            var validationResult = await _validator.ValidateAsync(stockReceive);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else
            {
                //compute inventories
                foreach (var stock in stockReceive.Stocks)
                {
                    var stockInventory = await _stockInventoryRepository.GetOneAsync(stock.MasterProductId, stock.BranchId);
                }
                ret = await _repository.AddAsync(stockReceive, cancellationToken);

            }
            await _unitOfWork.CommitAsync();
            return ret;
        }
    }
}
