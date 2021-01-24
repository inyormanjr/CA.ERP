using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.Common;
using CA.ERP.Domain.UnitOfWorkAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using OneOf;

namespace CA.ERP.Domain.TransactionAgg
{
  public class TransactionService : ServiceBase<Transaction>
  {
    public TransactionService(IUnitOfWork unitOfWork, IRepository<Transaction> repository, IValidator<Transaction> validator, IUserHelper userHelper) : base(unitOfWork, repository, validator, userHelper)
    {
    }

    public Task<OneOf<Guid, ValidationError, Forbidden>> CreateTransactionAsync(Guid branchId, TransactionType transactionType, InterestType interestType, DateTime transactionDate, DateTime deliveryDate, string transactionNumber, Guid salesmanId, Guid invenstigatedById, int total, int balance, int uDI, int totalRebate, int pN, int terms, int grossMonthly, int rebateMonthly, int netMonthly, List<TransactionProduct> transactionProducts, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}