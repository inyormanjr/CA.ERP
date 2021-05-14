using CA.ERP.Common.ErrorCodes;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.Services;
using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.StockReceiveAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.CommitDirectStockReceive
{
    public class CommitDirectStockReceiveHandler : IRequestHandler<CommitDirectStockReceiveCommand, DomainResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommitDirectStockReceive _commitDirectStockReceive;
        private readonly IStockReceiveRepository _stockReceiveRepository;
        private readonly IStockRepository _stockRepository;

        public CommitDirectStockReceiveHandler(IUnitOfWork unitOfWork, ICommitDirectStockReceive commitDirectStockReceive, IStockReceiveRepository stockReceiveRepository, IStockRepository stockRepository)
        {
            _unitOfWork = unitOfWork;
            _commitDirectStockReceive = commitDirectStockReceive;
            _stockReceiveRepository = stockReceiveRepository;
            _stockRepository = stockRepository;
        }

        public async Task<DomainResult> Handle(CommitDirectStockReceiveCommand request, CancellationToken cancellationToken)
        {
            var stockReceive = await _stockReceiveRepository.GetByIdAsync(request.Id, cancellationToken);

            if (stockReceive == null)
            {
                return DomainResult.Error(ErrorType.NotFound, StockReceiveErrorCodes.NotFound, "Stock Receive was not found");
            }

            if (stockReceive.StockSouce == Common.Types.StockSource.Direct)
            {
                return DomainResult.Error(StockReceiveErrorCodes.InvalidStockSource, "Stock source should be direct");
            }

            var commitResult = _commitDirectStockReceive.Commit(stockReceive);

            if (!commitResult.IsSuccess)
            {
                return commitResult.ConvertTo<Guid>();
            }

            foreach (var stock in commitResult.Result)
            {
                await _stockRepository.AddAsync(stock, cancellationToken);
            }

            await _unitOfWork.CommitAsync(cancellationToken);
            return DomainResult.Success();
        }
    }
}
