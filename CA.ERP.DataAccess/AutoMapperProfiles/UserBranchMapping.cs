using AutoMapper;
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
            CreateMap<Dal.UserBranch, Dom.UserAgg.UserBranch>()
                .ReverseMap();
        }
    }
}
