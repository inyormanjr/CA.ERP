using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CA.ERP.Domain.CustomerAgg;
using Microsoft.EntityFrameworkCore;

namespace CA.ERP.DataAccess.Repositories
{
  public class CustomerRepository : AbstractRepository<Customer, Entities.Customer>, ICustomerRepository
  {
    public CustomerRepository(CADataContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public Task<int> CountAsync(string firstName, string lastname, CancellationToken cancellationToken)
    {
      IQueryable<Entities.Customer> query = buildQuery(firstName, lastname);
      return query.CountAsync();
    }

    private IQueryable<Entities.Customer> buildQuery(string firstName, string lastname)
    {
      var query = _context.Customers.AsQueryable();
      if (!string.IsNullOrEmpty(firstName))
      {
        query = query.Where(c => c.FirstName.StartsWith(firstName));
      }
      if (!string.IsNullOrEmpty(lastname))
      {
        query = query.Where(c => c.LastName.StartsWith(lastname));
      }

      return query;
    }

    public Task<List<Customer>> GetManyAsync(string firstName, string lastname, int skip, int take, CancellationToken cancellationToken)
    {
      IQueryable<Entities.Customer> query = buildQuery(firstName, lastname);
      return query.OrderBy(c => c.LastName).Skip(skip).Take(take).Select(c => _mapper.Map<Customer>(c)).ToListAsync(cancellationToken);
    }
  }
}
