using System;
using System.Threading;
using System.Threading.Tasks;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.TransactionAgg.Calculators;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using FluentValidation.Validators;

namespace CA.ERP.Domain.TransactionAgg
{
  public class TransactionValidator : AbstractValidator<Transaction>
  {
    private readonly TransactionTotalCalculator _transactionTotalCalculator;
    private readonly IBranchRepository _branchRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUserRepository _userRepository;

    public TransactionValidator(
        TransactionTotalCalculator transactionTotalCalculator,
      IBranchRepository branchRepository,
      ITransactionRepository transactionRepository,
      IUserRepository userRepository)
    {
        RuleFor(t => t.BranchId).MustAsync(BranchMustExistAsync);
        RuleFor(t=>t.AccountNumber).NotEmpty().MustAsync(accountNumberNoDuplicateAsync);
        RuleFor(t => t.TransactionType).NotEmpty();
        RuleFor(t => t.TransactionDate).NotEmpty();
        RuleFor(t => t.DeliveryDate).NotEmpty().GreaterThan(t => t.TransactionDate);
        RuleFor(t => t.TransactionNumber).NotEmpty().MustAsync(transactionNumberNoDuplicateAsync);
        RuleFor(t => t.SalesmanId).MustAsync(UserMustExistAsync);
        RuleFor(t => t.InvestigatedById).MustAsync(UserMustExistAsync);
        RuleFor(t => t.Total).Must(ValidateTotal).WithMessage("Invalid client calculation.");


        _transactionTotalCalculator = transactionTotalCalculator;
        _branchRepository = branchRepository;
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
    }

    private bool ValidateTotal(Transaction transaction, decimal total)
    {
      decimal expected = _transactionTotalCalculator.Calculate(transaction);
      return total ==  expected;
    }

    private Task<bool> UserMustExistAsync(Guid userId, CancellationToken cancellationToken)
    {
      return _userRepository.ExistAsync(userId,cancellationToken:cancellationToken);
    }

    private async Task<bool> transactionNumberNoDuplicateAsync(string transactionNumber, CancellationToken cancellationToken)
    {
      return !await _transactionRepository.CheckTransactionNumberExistAsync(transactionNumber, cancellationToken);
    }

    private async Task<bool> accountNumberNoDuplicateAsync(string accountNumber, CancellationToken cancellationToken)
    {
      return !await _transactionRepository.CheckAcountNumberExistAsync(accountNumber, cancellationToken);
    }

    private Task<bool> BranchMustExistAsync(Guid branchId, CancellationToken cancellationToken)
    {
        return _branchRepository.ExistAsync(branchId, cancellationToken:cancellationToken);
    }
  }
}