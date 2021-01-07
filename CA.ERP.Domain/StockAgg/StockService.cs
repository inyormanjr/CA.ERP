using CA.ERP.Domain.Base;
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
        private readonly IStockNumberGenerator _stockNumberGenerator;

        public StockService(IUnitOfWork unitOfWork, IStockRepository repository, IValidator<Stock> validator, IUserHelper userHelper, IStockNumberGenerator stockNumberGenerator) : base(unitOfWork, repository, validator, userHelper)
        {
            _stockNumberGenerator = stockNumberGenerator;
        }

        public async Task<IEnumerable<string>> GenerateStockNumbersAsync(string prefix, string starting, int count)
        {
            return await _stockNumberGenerator.GenerateStockNumberAsync(prefix, starting, count);
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
    }
}
