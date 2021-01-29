using CA.ERP.Domain.Base;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Common;
using CA.ERP.Domain.Helpers;
using CA.ERP.Domain.StockCounterAgg;
using CA.ERP.Domain.StockReceiveAgg;
using CA.ERP.Domain.UnitOfWorkAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using FluentValidation.Results;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockAgg
{
    public class StockService : ServiceBase<Stock>
    {
    private readonly IDateTimeHelper _dateTimeHelper;
    private readonly IStockCounterFactory _stockCounterFactory;
        private readonly IBranchPermissionValidator<Branch> _branchBranchPermissionValidator;
        private readonly IBranchRepository _branchRepository;
        private readonly IStockCounterRepository _stockCounterRepository;
        private readonly IStockRepository _stockRepository;

        public StockService(IUnitOfWork unitOfWork, IDateTimeHelper dateTimeHelper, IStockCounterFactory stockCounterFactory, IBranchPermissionValidator<Branch> branchBranchPermissionValidator, IBranchRepository branchRepository, IStockCounterRepository stockCounterRepository, IStockRepository repository, IValidator<Stock> validator, IUserHelper userHelper) : base(unitOfWork, repository, validator, userHelper)
        {
      _dateTimeHelper = dateTimeHelper;
      _stockCounterFactory = stockCounterFactory;
            _branchBranchPermissionValidator = branchBranchPermissionValidator;
            _branchRepository = branchRepository;
            _stockCounterRepository = stockCounterRepository;
            _stockRepository = repository;
        }

        public async Task<StockNumberGenerator> GetStockNumberGenerator(Guid branchId)
        {
            var branchOption = await _branchRepository.GetByIdAsync(branchId);
            return await branchOption.Match<Task<StockNumberGenerator>>(
                f0: async branch => {
                    var stockCounterOption = await _stockCounterRepository.GetStockCounterAsync(branch.Code);
                    var stockCounter = stockCounterOption.Match(f0: st => st, f1: _ => _stockCounterFactory.CreateFresh(branch.Code));
                    return new StockNumberGenerator(_dateTimeHelper, stockCounter);

                        
                },
                f1: _ =>  throw new Exception("Branch Not Found")
            );
            
            
        }

        public async Task<IEnumerable<string>> GenerateStockNumbersAsync(Guid branchId, int count, CancellationToken cancellationToken)
        {
            var stockNumberGenerator = await GetStockNumberGenerator(branchId);
            var stockNumbers = await stockNumberGenerator.GenerateStockNumberAsync();
            var ret = stockNumbers.Take(count).ToList();
            await _stockCounterRepository.AddOrUpdateStockCounterAsync(stockNumberGenerator.StockCounter, cancellationToken);
            await _unitOfWork.CommitAsync();
            return ret;
        }

        public async Task<OneOf<Guid, List<ValidationFailure>, NotFound>> UpdateStockAsync(Guid id, Guid masterProductId, string serialNumber, decimal costPrice, CancellationToken cancellationToken)
        {
            var stockOption = await _repository.GetByIdAsync(id, cancellationToken: cancellationToken);
            return await stockOption.Match<Task<OneOf<Guid, List<ValidationFailure>, NotFound>>>(
                f0: async stock =>
                {

                    OneOf<Guid, List<ValidationFailure>, NotFound> ret;

                    stock.MasterProductId = masterProductId;
                    stock.SerialNumber = serialNumber;
                    stock.CostPrice = costPrice;

                    var validationResult = await _validator.ValidateAsync(stock);
                    if (!validationResult.IsValid)
                    {
                        ret = validationResult.Errors.ToList();
                    }
                    else
                    {
                        var updateResult = await _repository.UpdateAsync(id, stock, cancellationToken);
                        ret = updateResult.Match<OneOf<Guid, List<ValidationFailure>, NotFound>>(
                            f0: id => id,
                            f1: _ => default(NotFound)
                            );
                        await _unitOfWork.CommitAsync();
                        ret = stock.Id;
                    }

                    return ret;
                },
                f1: async  _ =>
                {
                    return  await Task.FromResult( default(NotFound));
                }
             );
        }

        public async Task<PaginationBase<Stock>> GetStocksAsync(string brand = null, string model = null, string stockNumber = null, string serial = null, int pageSize = 10, int page = 1, CancellationToken cancellationToken = default)
        {
            int skip = (page - 1) * pageSize;
            int take = pageSize;

            int count = await _stockRepository.CountAsync(brand , model, stockNumber, serial);
            IEnumerable<Stock> stocks = await _stockRepository.GetManyAsync(brand, model, stockNumber, serial, skip, take, cancellationToken);

            double totalPages = (double)count / (double)pageSize;
            return new PaginatedStocks() { 
                Data = stocks.ToList(),
                CurrentPage = page,
                TotalPage = (int)Math.Ceiling(totalPages),
                PageSize = pageSize,
                TotalCount = count,
            };
        }

        public async Task<List<Stock>> GetManyAsync(Guid branchId, List<Guid> stockIds, CancellationToken cancellationToken = default)
        {
            return await _stockRepository.GetManyAsync(branchId, stockIds, cancellationToken);
        }
    }
}
