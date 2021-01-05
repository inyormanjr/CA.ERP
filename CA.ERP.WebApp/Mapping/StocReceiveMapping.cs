using AutoMapper;
using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.StockReceiveAgg;
using CA.ERP.WebApp.Dto.Stock;
using CA.ERP.WebApp.Dto.StockReceive;

namespace CA.ERP.WebApp.Mapping
{
    public class StocReceiveMapping : Profile
    {
        public StocReceiveMapping()
        {
            
            CreateMap<StockReceiveCreate, StockReceive>();
        }
    }
}
