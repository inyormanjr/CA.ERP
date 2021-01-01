using AutoMapper;
using CA.ERP.Domain.PurchaseOrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal = CA.ERP.DataAccess.Entities;

namespace CA.ERP.DataAccess.AutoMapperProfiles
{
    public class PurchaseOrderMapping:Profile
    {
        public PurchaseOrderMapping()
        {
            CreateMap<Dal.PurchaseOrder, PurchaseOrder>()
                .ForMember(po => po.SupplierName, opt => opt.MapFrom(po => po.Supplier.Name))
                .ForMember(po => po.BranchName, opt => opt.MapFrom(po => po.Branch.Name))
                .ReverseMap();
                
            CreateMap<Dal.PurchaseOrderItem, PurchaseOrderItem>().ReverseMap();
        }
    }
}
