﻿using AutoMapper;
using CA.ERP.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Text;
using Dal = CA.ERP.DataAccess.Entities;
using Dom = CA.ERP.Domain;

namespace CA.ERP.DataAccess.AutoMapperProfiles
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<Dal.User, User>();
            CreateMap<User, Dal.User>();
        }
    }
}
