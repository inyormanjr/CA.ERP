using AutoMapper;
using CA.ERP.Domain.StockTransferAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal = CA.ERP.DataAccess.Entities;

namespace CA.ERP.DataAccess.Repositories
{
    public class StockTransferRepository : AbstractRepository<StockTransfer, Dal.StockTransfer>, IStockTransferRepository
    {
        public StockTransferRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {
        }


    }
}
