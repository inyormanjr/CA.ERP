using AutoMapper;
using CA.ERP.Domain.BranchAgg;
using System;
using System.Collections.Generic;
using System.Text;
using Dal = CA.ERP.DataAccess.Entities;
using Dom = CA.ERP.Domain;

namespace CA.ERP.DataAccess.AutoMapperProfiles
{
    public class BranchMapping : Profile
    {
        public BranchMapping()
        {
            CreateMap<Branch, Dal.Branch>();
            CreateMap<Dal.Branch, Branch>();
        }
    }
}
