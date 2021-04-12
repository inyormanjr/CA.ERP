using AutoMapper;
using CA.ERP.Domain.SupplierAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal = CA.ERP.DataAccess.Entities;

namespace CA.ERP.DataAccess.AutoMapperProfiles
{
    public class SupplierMapping: Profile
    {
        public SupplierMapping()
        {
            CreateMap<Dal.Supplier, Supplier>();


            CreateMap<Dal.SupplierBrand, SupplierBrand>()
                .ForMember(dest =>dest.BrandName, cfg => cfg.MapFrom(src => src.Brand.Name));


           // CreateMap<Dal.SupplierMasterProduct, SupplierMasterProduct>();
        }
    }
}
