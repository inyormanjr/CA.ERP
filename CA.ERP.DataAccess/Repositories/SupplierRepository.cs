﻿using AutoMapper;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.Common;
using CA.ERP.Domain.SupplierAgg;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dal = CA.ERP.DataAccess.Entities;

namespace CA.ERP.DataAccess.Repositories
{
    public class SupplierRepository : AbstractRepository<Supplier, Dal.Supplier>, ISupplierRepository
    {

        public SupplierRepository(CADataContext context, IMapper mapper):base(context, mapper)
        {

        }
    }
}
