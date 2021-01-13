using AutoMapper;
using CA.ERP.Domain.MasterProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto = CA.ERP.DataAccess.Entities;

namespace CA.ERP.DataAccess.Repositories
{
    public class MasterProductRepository: AbstractRepository<MasterProduct, Dto.MasterProduct>, IMasterProductRepository
    {
        public MasterProductRepository(CADataContext context, IMapper mapper)
            : base(context, mapper)
        {

        }
    }
}
