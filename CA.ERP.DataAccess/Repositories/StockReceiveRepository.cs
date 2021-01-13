﻿using AutoMapper;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal = CA.ERP.DataAccess.Entities;

namespace CA.ERP.DataAccess.Repositories
{
    public class StockReceiveRepository : AbstractRepository<StockReceive, Dal.StockReceive>, IStockReceiveRepository
    {
        public StockReceiveRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
