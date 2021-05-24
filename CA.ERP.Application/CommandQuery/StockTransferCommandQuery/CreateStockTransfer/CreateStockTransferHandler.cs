using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.IdentityAgg;
using CA.ERP.Domain.StockTransferAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.StockTransferCommandQuery.CreateStockTransfer
{
    public class CreateStockTransferHandler : IRequestHandler<CreateStockTransferCommand, DomainResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockTransferRepository _stockTransferRepository;
        private readonly IIdentityProvider _identityProvider;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateStockTransferHandler(IUnitOfWork unitOfWork, IStockTransferRepository stockTransferRepository, IIdentityProvider identityProvider, IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _stockTransferRepository = stockTransferRepository;
            _identityProvider = identityProvider;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<DomainResult<Guid>> Handle(CreateStockTransferCommand request, CancellationToken cancellationToken)
        {

            var currentIdentity = await _identityProvider.GetCurrentIdentity();
            var dtoStockTransfer = request.StockTransfer;
            var createResult = StockTransfer.Create(dtoStockTransfer.SourceBranchId, dtoStockTransfer.DestinationBranchId, currentIdentity.Id, _dateTimeProvider);

            if (!createResult.IsSuccess)
            {
                return createResult.ConvertTo<Guid>();
            }

            var stockTransfer = createResult.Result;

            foreach (var item in dtoStockTransfer.Items)
            {
                var createStockTransferItem = StockTransferItem.Create(stockTransfer.Id, item.StockId);
                if (!createStockTransferItem.IsSuccess)
                {
                    return createStockTransferItem.ConvertTo<Guid>();
                }

                stockTransfer.AddItem(createStockTransferItem.Result);
            }

            var id = await _stockTransferRepository.AddAsync(stockTransfer, cancellationToken);
            await _unitOfWork.CommitAsync();

            return id;
        }
    }
}
