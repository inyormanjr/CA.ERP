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
            CreateMap<Dto.PurchaseOrder.PurchaseOrderCreate, PurchaseOrder>();
            CreateMap<Dto.PurchaseOrder.PurchaseOrderItemCreate, PurchaseOrderItem>();

            CreateMap<Dto.PurchaseOrder.PurchaseOrderUpdate, PurchaseOrder>();
            CreateMap<Dto.PurchaseOrder.PurchaseOrderItemUpdate, PurchaseOrderItem>();

            CreateMap<PurchaseOrder, Dto.PurchaseOrder.PurchaseOrderView>();
            CreateMap<PurchaseOrderItem, Dto.PurchaseOrder.PurchaseOrderItemView> ();

            //report dto
            CreateMap<PurchaseOrder, ReportDto.PurchaseOrder>()
                .ForMember(dest => dest.Date, cfg => cfg.MapFrom(src => src.CreatedAt));
            CreateMap<PurchaseOrderItem, ReportDto.PurchaseOrderItem>();
        }
    }
}
