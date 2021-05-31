using CA.ERP.Common.ErrorCodes;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.IdentityAgg;
using CA.ERP.Domain.Services;
using CA.ERP.Domain.StockCounterAgg;
using CA.ERP.Domain.StockReceiveAgg;
using CA.ERP.Domain.StockTransferAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.GenerateStockReceiveFromStockTransfer
{
    public class GenerateStockReceiveFromStockTransferHandler : IRequestHandler<GenerateStockReceiveFromStockTransferCommand, DomainResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockReceiveRepository _stockReceiveRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IBranchRepository _branchRepository;
        private readonly IStockCounterRepository _stockCounterRepository;
        private readonly IIdentityProvider _identityProvider;
        private readonly IStockTransferRepository _stockTransferRepository;
        private readonly IStockReceiveGeneratorFromStockTransferService _stockReceiveGeneratorFromStockTransferService;

        public GenerateStockReceiveFromStockTransferHandler(IUnitOfWork unitOfWork, IStockReceiveRepository stockReceiveRepository, IDateTimeProvider dateTimeProvider, IBranchRepository branchRepository, IStockCounterRepository stockCounterRepository, IIdentityProvider identityProvider, IStockTransferRepository stockTransferRepository, IStockReceiveGeneratorFromStockTransferService stockReceiveGeneratorFromStockTransferService)
        {
            _unitOfWork = unitOfWork;
            _stockReceiveRepository = stockReceiveRepository;
            _dateTimeProvider = dateTimeProvider;
            _branchRepository = branchRepository;
            _stockCounterRepository = stockCounterRepository;
            _identityProvider = identityProvider;
            _stockTransferRepository = stockTransferRepository;
            _stockReceiveGeneratorFromStockTransferService = stockReceiveGeneratorFromStockTransferService;
        }

        public async Task<DomainResult<Guid>> Handle(GenerateStockReceiveFromStockTransferCommand request, CancellationToken cancellationToken)
        {
            var stockTransfer = await _stockTransferRepository.GetByIdAsync(request.StockTransferId);

            if (stockTransfer == null)
            {
                return DomainResult<Guid>.Error(ErrorType.NotFound, StockReceiveErrorCodes.NotFound, "Stock transfer was not found.");
            }
            var identity = await _identityProvider.GetCurrentIdentity();
            if (!identity.BelongsToBranch(stockTransfer.DestinationBranchId))
            {
                return DomainResult<Guid>.Error(ErrorType.Forbidden, IdentityErrorCodes.Forbidden, "Your are not assigned to this branch.");
            }
            var branch = await _branchRepository.GetByIdAsync(stockTransfer.DestinationBranchId);
            if (branch == null)
            {
                return DomainResult<Guid>.Error(BranchErrorCodes.NotFound, "Branch was not found.");
            }
            var stockCounter = await _stockCounterRepository.GetStockCounterAsync(branch.Code, cancellationToken);
            if (stockCounter == null && (stockCounter?.IsValid(_dateTimeProvider) ?? false) == false)
            {
                stockCounter = StockCounter.Create(branch.Code, _dateTimeProvider.GetCurrentDateTimeOffset().Year);
            }

            var createStockReceive = _stockReceiveGeneratorFromStockTransferService.Generate(stockTransfer, stockCounter);
            if (!createStockReceive.IsSuccess)
            {
                return createStockReceive.ConvertTo<Guid>();
            }

            var id = await _stockReceiveRepository.AddAsync(createStockReceive.Result, cancellationToken);
            await _stockCounterRepository.AddOrUpdateStockCounterAsync(stockCounter, cancellationToken);
            await _stockTransferRepository.UpdateAsync(request.StockTransferId, stockTransfer, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return DomainResult<Guid>.Success(id);
        }
    }
}
