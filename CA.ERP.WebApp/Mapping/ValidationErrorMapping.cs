using AutoMapper;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Mapping
{
    /// <summary>
    /// Maps Fluent validation failiure to dto ValidationError
    /// </summary>
    public class ValidationErrorMapping : Profile
    {
        /// <summary>
        /// Maps Fluent validation failiure to dto ValidationError
        /// </summary>
        public ValidationErrorMapping()
        {
            CreateMap<ValidationFailure, Dto.ValidationError>();
        }
    }
}
