using AutoMapper;
using CA.ERP.Domain.StockTransferAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal = CA.ERP.DataAccess.Entities;

namespace CA.ERP.DataAccess.AutoMapperProfiles
{
    public class StockTransferMapping: Profile
    {
        public StockTransferMapping()
        {
            CreateMap<StockTransfer, Dal.StockTransfer>();
            CreateMap<Dal.StockTransfer, StockTransfer>()
                .ForMember(dest => dest.DestinationBranchName, cfg => cfg.MapFrom(src => src.DestinationBranch.Name))
                .ForMember(dest => dest.SourceBranchName, cfg => cfg.MapFrom(src => src.SourceBranch.Name));

            CreateMap<StockTransferItem, Dal.StockTransferItem>();
            CreateMap<Dal.StockTransferItem, StockTransferItem>()
                .ForMember(dest => dest.BrandName, cfg => cfg.MapFrom(src => src.MasterProduct.Brand.Name))
                .ForMember(dest => dest.Model, cfg => cfg.MapFrom(src => src.MasterProduct.Model));
        }
    }
}
