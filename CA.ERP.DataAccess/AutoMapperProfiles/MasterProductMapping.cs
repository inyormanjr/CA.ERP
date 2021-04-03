using AutoMapper;
using CA.ERP.Domain.MasterProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal = CA.ERP.DataAccess.Entities;

namespace CA.ERP.DataAccess.AutoMapperProfiles
{
    public class MasterProductMapping : Profile
    {
        public MasterProductMapping()
        {
            CreateMap<MasterProduct, Dal.MasterProduct>();
        }
    }
}
