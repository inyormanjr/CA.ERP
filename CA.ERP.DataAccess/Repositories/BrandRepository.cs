using AutoMapper;
using CA.ERP.Domain.BrandAgg;
using CA.ERP.Domain.Common;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CA.ERP.Common.Extensions;
using Dal = CA.ERP.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CA.ERP.DataAccess.Repositories
{
    public class BrandRepository : AbstractRepository<Brand, Dal.Brand>, IBrandRepository
    {

        public BrandRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
