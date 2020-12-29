using AutoMapper;
using CA.ERP.Domain.PurchaseOrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Mapping
{
    public class PurchaseOrderMapping : Profile
    {
        public PurchaseOrderMapping()
        {
            CreateMap<Dto.CreatePurchaseOrderRequest, PurchaseOrder>();
            CreateMap<Dto.PurchaseOrderItemWrite, PurchaseOrderItem>();
        }
    }
}
