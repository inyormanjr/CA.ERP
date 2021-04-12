using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.Common;
using CA.ERP.Domain.Common.ResultTypes;
using CA.ERP.Domain.UnitOfWorkAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using OneOf;

namespace CA.ERP.Domain.TransactionAgg
{
  public class TransactionService : ServiceBase<Transaction>
  {
    private readonly IBranchPermissionValidator<Transaction> _transactionBranchPermissionValidator;

    public TransactionService(IUnitOfWork unitOfWork, IBranchPermissionValidator<Transaction> transactionBranchPermissionValidator, IRepository<Transaction> repository, IValidator<Transaction> validator, IUserHelper userHelper) : base(unitOfWork, repository, validator, userHelper)
    {
      _transactionBranchPermissionValidator = transactionBranchPermissionValidator;
    }

    public async Task<OneOf<Guid, ValidationError, Forbidden>> CreateTransactionAsync(Guid branchId, TransactionType transactionType, InterestType interestType, DateTime transactionDate, DateTime deliveryDate, string transactionNumber, Guid salesmanId, Guid investigatedById, decimal total, decimal down, decimal balance, decimal udi, decimal totalRebate, decimal principalAmount, decimal terms, decimal grossMonthly, decimal rebateMonthly, decimal netMonthly, List<TransactionProduct> transactionProducts, CancellationToken cancellationToken)
    {
      var transaction = new Transaction()
      {
        BranchId = branchId,
        TransactionType = transactionType,
        InterestType = interestType,
        TransactionDate = transactionDate,
        DeliveryDate = deliveryDate,
        TransactionNumber = transactionNumber,
        SalesmanId = salesmanId,
        InvestigatedById = investigatedById,
        Total = total,
        Down = down,
        Balance = balance,
        Udi = udi,
        TotalRebate = totalRebate,
        PrincipalAmount = principalAmount,
        Terms = terms,
        GrossMonthly = grossMonthly,
        RebateMonthly = rebateMonthly,
        NetMonthly = netMonthly,
        TransactionProducts = transactionProducts
      };

      OneOf<Guid, ValidationError, Forbidden> ret;

      if (!await _transactionBranchPermissionValidator.HasPermissionAsync(transaction, cancellationToken))
      {
        ret = default(Forbidden);
      }
      else
      {
        var validationResult = await _validator.ValidateAsync(transaction);
        if (!validationResult.IsValid)
        {
          ret = new ValidationError(validationResult.Errors.ToList());
        }
        else
        {
          ret = await _repository.AddAsync(transaction, cancellationToken);
          await _unitOfWork.CommitAsync();
        }
      }

      return ret;

    }
  }
}
