using AutoMapper;
using CA.ERP.Domain.BrandAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto = CA.ERP.Shared.Dto;

namespace CA.ERP.WebApp.Mapping
{
    public class BrandMapping : Profile
    {
        public BrandMapping()
        {
            CreateMap<Brand, Dto.Brand.BrandView>();
        }

    }
}
