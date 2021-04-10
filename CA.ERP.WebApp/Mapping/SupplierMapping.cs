using AutoMapper;
using CA.ERP.Domain.SupplierAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto = CA.ERP.Shared.Dto;

namespace CA.ERP.WebApp.Mapping
{
    public class SupplierMapping: Profile
    {
        public SupplierMapping()
        {
            CreateMap<Supplier, Dto.Supplier.SupplierView>().ReverseMap();
            CreateMap<SupplierBrand, Dto.Supplier.SupplierBrandView>().ReverseMap();
            CreateMap<SupplierMasterProduct, Dto.Supplier.SupplierMasterProductView>();
        }
    }
}
