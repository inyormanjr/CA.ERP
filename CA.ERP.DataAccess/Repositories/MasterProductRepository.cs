using AutoMapper;
using CA.ERP.Domain.MasterProductAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public async Task<MasterProduct> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            MasterProduct ret = null;

            var queryable = _context.MasterProducts.AsQueryable();

            var entity = await queryable.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
            if (entity != null)
            {
                ret = _mapper.Map<MasterProduct>(entity);
            }
            return ret;
        }
    }
}
