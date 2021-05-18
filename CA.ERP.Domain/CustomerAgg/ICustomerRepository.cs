using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CA.ERP.Domain.Core.Repository;

namespace CA.ERP.Domain.CustomerAgg
{
  public interface ICustomerRepository : IRepository<Customer>
  {
    Task<int> CountAsync(string firstName, string lastname, CancellationToken cancellationToken);
    Task<List<Customer>> GetManyAsync(string firstName, string lastname, int skip, int take, CancellationToken cancellationToken);
  }
}
