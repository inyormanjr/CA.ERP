using AutoMapper;
using CA.ERP.Domain.StockReceiveAgg;
using CA.ERP.Shared.Dto.StockReceive;

namespace CA.ERP.WebApp.Mapping
{
    public class StocReceiveMapping : Profile
    {
        public StocReceiveMapping()
        {
            
            CreateMap<StockReceive, StockReceiveView>();
            CreateMap<StockReceiveItem, StockReceiveItemView>();
        }
    }
}
