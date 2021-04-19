using CA.ERP.Common.Types;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.Entity;
using CA.ERP.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.SupplierAgg
{
    public class Supplier: IEntity
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }

        public List<SupplierBrand> SupplierBrands { get; set; } = new List<SupplierBrand>();
        public List<SupplierMasterProduct> SupplierMasterProducts { get; set; } = new List<SupplierMasterProduct>();

        
    }
}
