using System.Threading;
using System.Threading.Tasks;
using CA.ERP.Domain.Base;

namespace CA.ERP.Domain.TransactionAgg
{
  public interface ITransactionRepository : IRepository<Transaction>
  {
    Task<bool> CheckAcountNumberExistAsync(string accountNumber, CancellationToken cancellationToken);
    Task<bool> CheckTransactionNumberExistAsync(string transactionNumber, CancellationToken cancellationToken);
  }
}
