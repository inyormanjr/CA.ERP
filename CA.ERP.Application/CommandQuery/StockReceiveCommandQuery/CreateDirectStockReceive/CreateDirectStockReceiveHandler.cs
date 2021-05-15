using AutoMapper;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.Services;
using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.StockReceiveAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.CreateDirectStockReceive
{
    public class CreateDirectStockReceiveHandler : IRequestHandler<CreateDirectStockReceiveCommand, DomainResult<Guid>>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockNumberService _stockNumberService;
        private readonly IStockCounterRepository _stockCounterRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IStockReceiveRepository _stockReceiveRepository;
        private readonly ICommitDirectStockReceive _commitDirectStockReceive;
        private readonly IStockRepository _stockRepository;

        public CreateDirectStockReceiveHandler(IDateTimeProvider dateTimeProvider, IUnitOfWork unitOfWork, IStockNumberService stockNumberService, IStockCounterRepository stockCounterRepository, IBranchRepository branchRepository, IStockReceiveRepository stockReceiveRepository, ICommitDirectStockReceive commitDirectStockReceive, IStockRepository stockRepository)
        {
            _dateTimeProvider = dateTimeProvider;
            _unitOfWork = unitOfWork;
            _stockNumberService = stockNumberService;
            _stockCounterRepository = stockCounterRepository;
            _branchRepository = branchRepository;
            _stockReceiveRepository = stockReceiveRepository;
            _commitDirectStockReceive = commitDirectStockReceive;
            _stockRepository = stockRepository;
        }

        public async Task<DomainResult<Guid>> Handle(CreateDirectStockReceiveCommand request, CancellationToken cancellationToken)
        {
            var dto = request.StockReceive;

            var createResult = StockReceive.Create(dto.PurchaseOrderId, dto.BranchId, Common.Types.StockSource.Direct, dto.SupplierId, _dateTimeProvider);
            if (!createResult.IsSuccess)
            {
                return createResult.ConvertTo<Guid>();
            }

            var stockReceive = createResult.Result;
            var branch = await _branchRepository.GetByIdAsync(stockReceive.BranchId, cancellationToken);
            var stockCounter = await _stockCounterRepository.GetStockCounterAsync(branch.Code, cancellationToken);

            var stockNumbers = _stockNumberService.GenerateStockNumbers(stockCounter);

            foreach (var item in dto.Items)
            {
                var createItemResult = StockReceiveItem.Create(item.MasterProductId, stockReceive.Id, null, dto.BranchId, item.CostPrice, stockNumbers.FirstOrDefault(), item.BrandName, item.Model);
                if (!createItemResult.IsSuccess)
                {
                    return createItemResult.ConvertTo<Guid>();
                }

                stockReceive.AddItem(createItemResult.Result);
            }

            var id = await _stockReceiveRepository.AddAsync(stockReceive, cancellationToken);
            await _stockCounterRepository.AddOrUpdateStockCounterAsync(stockCounter, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);
            return DomainResult<Guid>.Success(id);
        }
    }
}
