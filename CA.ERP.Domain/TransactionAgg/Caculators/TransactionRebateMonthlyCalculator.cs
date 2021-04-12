using System.Linq;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.RebateMonthlyAgg;

namespace CA.ERP.Domain.TransactionAgg.Caculators
{
    public class TransactionRebateMonthlyCalculator : IHelper
    {
        private readonly IRebateMonthlyRepository _rebateRepository;
        public TransactionRebateMonthlyCalculator(IRebateMonthlyRepository rebateRepository)
        {
            _rebateRepository = rebateRepository;
        }
        public decimal Calculate(Transaction transaction)
        {
            var rebateMonthly =  _rebateRepository.GetRebateMonthlyList().FirstOrDefault(rm => rm.Min <= transaction.NetMonthly && rm.Max >= transaction.NetMonthly);
            return rebateMonthly?.Rebate ?? 0;
        }
    }
}