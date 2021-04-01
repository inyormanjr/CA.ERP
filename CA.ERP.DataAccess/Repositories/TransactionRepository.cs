using CA.ERP.Domain.Common;
using CA.ERP.Domain.TransactionAgg;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Repositories
{
  public class TransactionRepository : ITransactionRepository
  {
    public Task<Guid> AddAsync(Transaction entity, CancellationToken cancellationToken = default)
    {
      throw new NotImplementedException();
    }

    public Task<bool> CheckAcountNumberExistAsync(string accountNumber, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<bool> CheckTransactionNumberExistAsync(string transactionNumber, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<OneOf<Success, None>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
      throw new NotImplementedException();
    }

    public Task<bool> ExistAsync(Guid id, Status status = Status.Active, CancellationToken cancellationToken = default)
    {
      throw new NotImplementedException();
    }

    public Task<OneOf<Transaction, None>> GetByIdAsync(Guid id, Status status = Status.Active, CancellationToken cancellationToken = default)
    {
      throw new NotImplementedException();
    }

    public Task<List<Transaction>> GetManyAsync(int skip = 0, int take = int.MaxValue, Status status = Status.Active, CancellationToken cancellationToken = default)
    {
      throw new NotImplementedException();
    }

    public Task<OneOf<Guid, None>> UpdateAsync(Guid Id, Transaction entity, CancellationToken cancellationToken = default)
    {
      throw new NotImplementedException();
    }
  }
}
