using AutoMapper;
using CA.ERP.Domain.StockAgg;
using CA.ERP.Shared.Dto.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Mapping
{
    public class StockMapping : Profile
    {
        public StockMapping()
        {
            CreateMap<Stock, StockView>();

            //CreateMap<Stock, StockListItem>()
            //    .ForMember(dest => dest.Status, cfg => cfg.MapFrom(src => src.StockStatus.ToString()));
        }
    }
}
