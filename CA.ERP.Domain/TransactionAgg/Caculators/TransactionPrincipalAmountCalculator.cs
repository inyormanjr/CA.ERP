using CA.ERP.Domain.Base;

namespace CA.ERP.Domain.TransactionAgg.Caculators
{
    public class TransactionPrincipalAmountCalculator : IHelper
    {
        public decimal Calculate(Transaction transaction)
        {
            return transaction.GrossMonthly * transaction.Terms;
        }
    }
}