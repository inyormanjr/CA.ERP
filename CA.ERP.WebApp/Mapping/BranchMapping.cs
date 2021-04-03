using AutoMapper;
using CA.ERP.Domain.BranchAgg;
using Dto = CA.ERP.WebApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Mapping
{
    public class BranchMapping : Profile
    {
        public BranchMapping()
        {
            CreateMap<Branch, Dto.Branch.BranchView>();
        }
    }
}
