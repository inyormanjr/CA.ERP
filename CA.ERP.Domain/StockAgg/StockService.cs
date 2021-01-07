using CA.ERP.Domain.Base;
using CA.ERP.Domain.UnitOfWorkAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
