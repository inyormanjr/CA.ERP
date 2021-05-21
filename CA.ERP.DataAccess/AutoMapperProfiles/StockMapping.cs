using AutoMapper;
using CA.ERP.Domain.StockAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal = CA.ERP.DataAccess.Entities;

namespace CA.ERP.DataAccess.AutoMapperProfiles
{
    public class StockMapping: Profile
    {
        public StockMapping()
        {
            CreateMap<Stock, Dal.Stock>();
            CreateMap<Dal.Stock, Stock>()
                .ForMember(s=>s.BranchName, opt => opt.MapFrom(s=>s.Branch.Name))
                .ForMember(s=>s.BrandName, opt => opt.MapFrom(s=>s.MasterProduct.Brand.Name))
                .ForMember(s=>s.Model, opt => opt.MapFrom(s=>s.MasterProduct.Model));
        }
    }
}
