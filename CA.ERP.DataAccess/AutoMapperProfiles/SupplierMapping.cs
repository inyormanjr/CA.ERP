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
            CreateMap<Supplier, Dal.Supplier>();

            CreateMap<Dal.SupplierBrand, SupplierBrand>();
            CreateMap<SupplierBrand, Dal.SupplierBrand>();

            CreateMap<Dal.SupplierMasterProduct, SupplierMasterProduct>();
            CreateMap<SupplierMasterProduct, Dal.SupplierMasterProduct>()
                .ForMember(dalSmp => dalSmp.Id, option => option.Ignore());
        }
    }
}
