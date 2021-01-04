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
            CreateMap<Supplier, Dto.Supplier.SupplierView>().ReverseMap();
            CreateMap<SupplierBrand, Dto.Supplier.SupplierBrandView>().ReverseMap();

            CreateMap<Dto.Supplier.SupplierCreate, Supplier>();
            CreateMap<Dto.Supplier.SupplierBrandCreate, SupplierBrand>();

            CreateMap<Dto.Supplier.SupplierUpdate, Supplier>();
            CreateMap<Dto.Supplier.SupplierBrandUpdate, SupplierBrand>();

            CreateMap<Dto.Supplier.SupplierMasterProductUpdate, SupplierMasterProduct>();

        }
    }
}
