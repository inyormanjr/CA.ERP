using AutoMapper;
using CA.ERP.Domain.StockReceiveAgg;
using CA.ERP.Shared.Dto.StockReceive;

namespace CA.ERP.WebApp.Mapping
{
    public class StocReceiveMapping : Profile
    {
        public StocReceiveMapping()
        {
            
            CreateMap<StockReceive, StockReceiveView>().ForMember(dest => dest.StockSource, cfg => cfg.MapFrom(src => src.StockSouce));
            CreateMap<StockReceiveItem, StockReceiveItemView>();
        }
    }
}
