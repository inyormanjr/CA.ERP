using AutoMapper;
using CA.ERP.Domain.StockAgg;
using CA.ERP.WebApp.Dto.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Mapping
{
    public class StockMocking : Profile
    {
        public StockMocking()
        {
            CreateMap<StockCreate, Stock>();
        }
    }
}
