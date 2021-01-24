using CA.ERP.Domain.Base;
using CA.ERP.Domain.Common;
using CA.ERP.Domain.Common.ResultTypes;
using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.StockInventoryAgg;
using CA.ERP.Domain.StockMoveAgg;
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
        private readonly IStockMoveRepository _stockMoveRepository;
        private readonly StockInventoryService _stockInventoryService;
        private readonly IStockReceiveFactory _stockReceiveFactory;
        private readonly IStockReceiveStockMoveGenerator _stockInventoryStockReceiveCalculator;
        private readonly IBranchPermissionValidator<StockReceive> _branchPermissionValidator;

        public StockReceiveService(
            IUnitOfWork unitOfWork,
            IRepository<StockReceive> repository,
            IStockInventoryRepository stockInventoryRepository,
            IStockMoveRepository stockMoveRepository,
            IValidator<StockReceive> validator,
            IUserHelper userHelper,
            StockInventoryService stockInventoryService,
            IStockReceiveFactory stockReceiveFactory,
            IStockReceiveStockMoveGenerator stockInventoryStockReceiveCalculator,
            IBranchPermissionValidator<StockReceive> branchPermissionValidator) 
            : base(unitOfWork, repository, validator, userHelper)
        {
            _stockInventoryRepository = stockInventoryRepository;
            _stockMoveRepository = stockMoveRepository;
            _stockInventoryService = stockInventoryService;
            _stockReceiveFactory = stockReceiveFactory;
            _stockInventoryStockReceiveCalculator = stockInventoryStockReceiveCalculator;
            _branchPermissionValidator = branchPermissionValidator;
        }

        public async Task<OneOf<Guid, List<ValidationFailure>,  Forbidden>> CreateStockReceive(Guid? purchaseOrderId, Guid branchId, StockSource stockSource, Guid supplierId, string deliveryReference, List<Stock> stocks, CancellationToken cancellationToken)
        {
            OneOf<Guid, List<ValidationFailure>, Forbidden> ret;
            StockReceive stockReceive = _stockReceiveFactory.CreateStockReceive(purchaseOrderId, branchId, stockSource, supplierId, deliveryReference, stocks);

 
            var validationResult = await _validator.ValidateAsync(stockReceive);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else if (!await _branchPermissionValidator.HasPermissionAsync(stockReceive))
            {
                ret = default(Forbidden);
            }
            else
            {
                //compute inventories
                foreach (var stock in stockReceive.Stocks)
                {
                    await _stockInventoryService.CalculateStockInventoryForStockReceive(stockReceive.Id,  stock, cancellationToken);
                }
                ret = await _repository.AddAsync(stockReceive, cancellationToken);
                await _unitOfWork.CommitAsync();
            }
            
            return ret;
        }
    }
}
