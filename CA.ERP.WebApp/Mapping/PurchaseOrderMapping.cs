using AutoMapper;
using CA.ERP.Domain.PurchaseOrderAgg;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dto = CA.ERP.Shared.Dto;

namespace CA.ERP.WebApp.Mapping
{
    public class PurchaseOrderMapping : Profile
    {
        public PurchaseOrderMapping()
        {

            CreateMap<PurchaseOrder, Dto.PurchaseOrder.PurchaseOrderView>();
            CreateMap<PurchaseOrderItem, Dto.PurchaseOrder.PurchaseOrderItemView> ();

            //report dto
            CreateMap<PurchaseOrder, ReportDto.PurchaseOrder>();
            CreateMap<PurchaseOrderItem, ReportDto.PurchaseOrderItem>();
        }
    }
}
