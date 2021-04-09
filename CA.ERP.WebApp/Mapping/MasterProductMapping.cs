using AutoMapper;
using CA.ERP.Domain.MasterProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto = CA.ERP.Shared.Dto;

namespace CA.ERP.WebApp.Mapping
{
    public class MasterProductMapping : Profile
    {
        public MasterProductMapping()
        {

            CreateMap<MasterProduct, Dto.MasterProduct.MasterProductView>();
        }
    }
}
