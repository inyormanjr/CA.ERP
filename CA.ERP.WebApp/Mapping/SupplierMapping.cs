using AutoMapper;
using CA.ERP.Domain.SupplierAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Mapping
{
    public class SupplierMapping: Profile
    {
        public SupplierMapping()
        {
            CreateMap<Supplier, Dto.Supplier>().ReverseMap();
            CreateMap<SupplierBrand, Dto.SupplierBrand>().ReverseMap();
        }
    }
}
