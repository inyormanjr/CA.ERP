﻿using AutoMapper;
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
            CreateMap<Dto.User.UserUpdate, User>();
            CreateMap<Dto.User.UserCreate, User>();

            CreateMap<User, Dto.User.UserView>();

            CreateMap<UserBranch, Dto.User.UserBranchView>();
        }
    }
}
