using AutoMapper;
using CA.ERP.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal = CA.ERP.DataAccess.Entities;
using Dom = CA.ERP.Domain;

namespace CA.ERP.DataAccess.AutoMapperProfiles
{
    public class UserBranchMapping : Profile
    {
        public UserBranchMapping()
        {
            CreateMap<Dal.UserBranch, UserBranch>()
                .ForMember(dest => dest.Name, cfg => cfg.MapFrom(src => src.Branch.Name))
                .ForMember(dest => dest.Code, cfg => cfg.MapFrom(src => src.Branch.Code));
            CreateMap<UserBranch, Dal.UserBranch>();
                
        }
    }
}
