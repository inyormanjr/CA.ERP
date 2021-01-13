using AutoMapper;
using CA.ERP.Domain.BrandAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal = CA.ERP.DataAccess.Entities;

namespace CA.ERP.DataAccess.AutoMapperProfiles
{
    public class BrandMapping: Profile
    {
        public BrandMapping()
        {
            CreateMap<Brand, Dal.Brand>();
            CreateMap<Dal.Brand, Brand>();
        }
    }
}
