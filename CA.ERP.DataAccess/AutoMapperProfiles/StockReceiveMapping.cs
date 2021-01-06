using AutoMapper;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal = CA.ERP.DataAccess.Entities;

namespace CA.ERP.DataAccess.AutoMapperProfiles
{
    public class StockReceiveMapping : Profile
    {
        public StockReceiveMapping()
        {
            CreateMap<StockReceive, Dal.StockReceive>();
            CreateMap<Dal.StockReceive, StockReceive>();
        }
    }
}
