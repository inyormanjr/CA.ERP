using AutoMapper;
using CA.ERP.Domain.StockInventoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.AutoMapperProfiles
{
    public class StockInventoryMapping:Profile
    {
        public StockInventoryMapping()
        {
            CreateMap<StockInventory, Entities.StockInventory>();
            CreateMap<Entities.StockInventory, StockInventory>();
        }
    }
}
