using System.Linq;
using CA.ERP.Domain.Base;

namespace CA.ERP.Domain.TransactionAgg.Caculators
{
    public class TransactionDownCalculator : IHelper
    {
        public decimal Calculate(Transaction transaction){
            return transaction.TransactionProducts.Select(tp => tp.DownPayment).DefaultIfEmpty(0).Sum();
        }
    }
}