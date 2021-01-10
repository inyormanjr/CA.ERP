using AutoMapper;
using CA.ERP.Domain.StockMoveAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.AutoMapperProfiles
{
    public class StockMoveMapping : Profile
    {
        public StockMoveMapping()
        {
            CreateMap<StockMove, Entities.StockMove>();
            CreateMap<Entities.StockMove, StockMove>();

        }
    }
}
