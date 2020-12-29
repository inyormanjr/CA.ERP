using AutoMapper;
using CA.ERP.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<Dto.UserUpdate, User>()
                .ReverseMap();

            CreateMap<Dto.User, User>()
                .ReverseMap();
        }
    }
}
