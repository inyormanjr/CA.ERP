using System;
using System.Collections.Generic;
using CA.ERP.Domain.TransactionAgg;

namespace CA.ERP.WebApp.Dto.Transaction
{
  public class TransactionCreate
  {
    public Guid BranchId { get; set; }
    public string AccountNumber { get; set; } 
    public TransactionType TransactionType { get; set; }
    public InterestType InterestType { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string TransactionNumber { get; set; }
    public Guid SalesmanId { get; set; }
    public Guid InvestigatedById { get; set; }
    public decimal Total { get; set; }
    public decimal Down { get; set; }
    public decimal Balance { get; set; }
    public decimal Udi { get; set; }
    public decimal TotalRebate { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal Terms { get; set; }
    public decimal GrossMonthly { get; set; }
    public decimal RebateMonthly { get; set; }
    public decimal NetMonthly { get; set; }
    public List<TransactionProductCreate> TransactionProducts { get; set; }
    
  }
}