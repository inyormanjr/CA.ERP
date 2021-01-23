﻿using System;
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
        public int Total { get; set; }
        public int Balance { get; set; }
        public int UDI { get; set; }
        public int TotalRebate { get; set; }
        public int PN { get; set; }
        public int Terms { get; set; }
        public int GrossMonthly { get; set; }
        public int RebateMonthly { get; set; }
        public int NetMonthly { get; set; }
        public List<Payment> Payments { get;  set; }
        public List<Product> Products { get; set; }
    } 
}
