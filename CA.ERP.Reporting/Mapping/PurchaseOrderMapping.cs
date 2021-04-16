using AutoMapper;
using CA.ERP.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Reporting.Mapping
{
    public class PurchaseOrderMapping : Profile
    {
        public PurchaseOrderMapping()
        {
            CreateMap<PurchaseOrder, Dto.PurchaseOrder>();
            CreateMap<PurchaseOrderItem, Dto.PurchaseOrderItem>();
        }
    }
}
