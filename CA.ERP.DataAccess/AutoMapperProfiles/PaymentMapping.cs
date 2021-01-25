using AutoMapper;
using CA.ERP.Domain.PaymentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.AutoMapperProfiles
{
    public class PaymentMapping: Profile
    {
        public PaymentMapping()
        {
            CreateMap<Payment, Entities.Payment>();
            CreateMap<Entities.Payment, Payment>();


            CreateMap<CashPaymentDetail, Entities.CashPaymentDetail>();
            CreateMap<Entities.CashPaymentDetail, CashPaymentDetail>();

            CreateMap<CardPaymentDetail, Entities.CardPaymentDetail>();
            CreateMap<Entities.CardPaymentDetail, CardPaymentDetail>();

            CreateMap<ChequePaymentDetail, Entities.ChequePaymentDetail>();
            CreateMap<Entities.ChequePaymentDetail, ChequePaymentDetail>();
        }
    }
}
