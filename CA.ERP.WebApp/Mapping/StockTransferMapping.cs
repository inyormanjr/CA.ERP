using AutoMapper;
using CA.ERP.Domain.StockTransferAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto = CA.ERP.Shared.Dto.StockTransfer;

namespace CA.ERP.WebApp.Mapping
{
    public class StockTransferMapping : Profile
    {
        public StockTransferMapping()
        {
            CreateMap<StockTransfer, Dto.StockTransferView>();
            CreateMap<StockTransferItem, Dto.StockTransferItemView>();
        }
    }
}
