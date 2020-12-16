using AutoMapper;
using CA.ERP.Lib.Domain.BranchAgg;
using CA.ERP.Lib.Domain.UserAgg;
using CA.ERP.WebApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserRegistrationDTO>().ReverseMap();
            CreateMap<Branch, Branch>();
        }
    }
}
