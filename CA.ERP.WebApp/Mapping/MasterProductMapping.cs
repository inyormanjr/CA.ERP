using AutoMapper;
using CA.ERP.Domain.MasterProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Mapping
{
    public class MasterProductMapping : Profile
    {
        public MasterProductMapping()
        {
            CreateMap<Dto.MasterProduct.MasterProductCreate, MasterProduct>().ReverseMap();
            CreateMap<Dto.MasterProduct.MasterProductUpdate, MasterProduct>().ReverseMap();
            CreateMap<MasterProduct, Dto.MasterProduct.MasterProductView>().ReverseMap();
        }
    }
}
