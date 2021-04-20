using AutoMapper;
using CA.ERP.Domain.StockCounterAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.AutoMapperProfiles
{
    public class StockCounterMapping: Profile
    {
        public StockCounterMapping()
        {
            CreateMap<StockCounter, Entities.StockCounter>();
            CreateMap<Entities.StockCounter, StockCounter>();
        }
    }
}
