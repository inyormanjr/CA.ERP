using CA.ERP.Domain.Base;

namespace CA.ERP.Domain.TransactionAgg.Caculators
{
    public class TransactionBalanceCalculator : IHelper
    {
        public decimal Calculate(Transaction transaction)
        {
            return transaction.Total - transaction.Down;
        }
    }
}