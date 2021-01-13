using CA.ERP.Domain.Base;
using CA.ERP.Domain.StockAgg;
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
        private readonly IStockReceiveFactory _stockReceiveFactory;

        public StockReceiveService(IUnitOfWork unitOfWork ,IRepository<StockReceive> repository, IValidator<StockReceive> validator, IUserHelper userHelper, IStockReceiveFactory stockReceiveFactory) : base(unitOfWork, repository, validator, userHelper)
        {
            _stockReceiveFactory = stockReceiveFactory;
        }

        public async Task<OneOf<Guid, List<ValidationFailure>>> CreateStockReceive(Guid? purchaseOrderId, Guid branchId, StockSource stockSource, List<Stock> stocks, CancellationToken cancellationToken)
        {
            OneOf<Guid, List<ValidationFailure>> ret;
            StockReceive stockReceive = _stockReceiveFactory.CreateStockReceive(purchaseOrderId, branchId, stockSource, stocks);
            
            var validationResult = await _validator.ValidateAsync(stockReceive);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else
            {
                ret = await _repository.AddAsync(stockReceive, cancellationToken);
            }
            await _unitOfWork.CommitAsync();
            return ret;
        }
    }
}
