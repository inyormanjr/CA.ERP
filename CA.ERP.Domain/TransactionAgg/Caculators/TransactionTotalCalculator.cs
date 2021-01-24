using System.Linq;
using CA.ERP.Domain.Base;

namespace CA.ERP.Domain.TransactionAgg.Calculators
{
    public class TransactionTotalCalculator : IHelper
    {
        public decimal Calculate(Transaction transaction)
        {
            return transaction.TransactionProducts.Sum(t => t.SalePrice);
        }
    }
}