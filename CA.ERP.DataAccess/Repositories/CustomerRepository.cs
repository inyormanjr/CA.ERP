using AutoMapper;
using CA.ERP.Domain.CustomerAgg;

namespace CA.ERP.DataAccess.Repositories
{
  public class CustomerRepository : AbstractRepository<Customer, Entities.Customer>, ICustomerRepository
  {
    public CustomerRepository(CADataContext context, IMapper mapper) : base(context, mapper)
    {
    }
  }
}