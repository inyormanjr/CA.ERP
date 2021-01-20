using AutoMapper;
using CA.ERP.Domain.BankAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Repositories
{
    public class BankRepository : AbstractRepository<Bank, Entities.Bank>, IBankRepository
    {
        public BankRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
