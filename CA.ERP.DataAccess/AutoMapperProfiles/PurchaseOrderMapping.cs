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
            CreateMap<Dal.PurchaseOrder, PurchaseOrder>().ReverseMap();
            CreateMap<Dal.PurchaseOrderItem, PurchaseOrderItem>().ReverseMap();
        }
    }
}
