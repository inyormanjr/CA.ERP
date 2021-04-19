using CA.ERP.Common.Types;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.Core.Entity;
using CA.ERP.Domain.MasterProductAgg;
using CA.ERP.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.BrandAgg
{
    public class Brand : IEntity
    {


        public string Name { get; set; }
        public string Description { get; set; }

        public Guid Id { get; private set; }

        public Status Status { get; private set; }

        protected Brand(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public DomainResult Update(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                return DomainResult.Error(BrandErrorCodes.InvalidName, $"'{nameof(name)}' cannot be null or empty.");

            }
            Name = name;
            Description = description;

            return DomainResult.Success();
        }

        public static DomainResult<Brand> Create(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                return DomainResult<Brand>.Error(BrandErrorCodes.InvalidName, $"'{nameof(name)}' cannot be null or empty.");

            }
            var brand = new Brand(name, description);
            return DomainResult<Brand>.Success(brand);
        }

    }
}
