using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA.ERP.Domain.TransactionAgg;

namespace CA.ERP.DataAccess.Entities
{
    public class Transaction: EntityBase
    {
        public string AccountNumber { get; set; }
        public Guid BranchId { get; set; }
        public TransactionType TransactionType { get; set; }
        public InterestType InterestType { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string TransactionNumber { get; set; }
        public Guid SalesmanId { get; set; }
        public Guid InvenstigatedById { get; set; }
        public decimal Total { get; set; }
        public decimal Balance { get; set; }
        public decimal UDI { get; set; }
        public decimal TotalRebate { get; set; }
        public decimal PN { get; set; }
        public decimal Terms { get; set; }
        public decimal GrossMonthly { get; set; }
        public decimal RebateMonthly { get; set; }
        public decimal NetMonthly { get; set; }
        public List<Payment> Payments { get;  set; }
        public List<TransactionProduct> Products { get; set; }
    } 
}
