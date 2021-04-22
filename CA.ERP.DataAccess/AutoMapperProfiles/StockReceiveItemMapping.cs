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
    public class StockReceiveItemMapping : Profile
    {
        public StockReceiveItemMapping()
        {
            CreateMap<StockReceiveItem, Dal.StockReceiveItem>();
            CreateMap<Dal.StockReceiveItem, StockReceiveItem>()
                .ForMember(dest => dest.BrandName, cfg => cfg.MapFrom(src => src.Branch.Name))
                .ForMember(dest => dest.Model, cfg => cfg.MapFrom(src => src.MasterProduct.Model));
        }
    }
}
