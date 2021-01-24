using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CA.ERP.Common.Extensions;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.UserAgg;

namespace CA.ERP.Domain.TransactionAgg
{
  public class TransactionBranchPermissionValidator : IBranchPermissionValidator<Transaction>
  {
    

    private readonly IUserHelper _userHelper;
    private readonly IUserRepository _userRepository;
    private readonly IStockRepository _stockRepository;

    public TransactionBranchPermissionValidator(IUserHelper userHelper, IUserRepository userRepository, IStockRepository stockRepository)
    {
        _userHelper = userHelper;
        _userRepository = userRepository;
        _stockRepository = stockRepository;
    }

    public async Task<bool> HasPermissionAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        transaction.ThrowIfNullArgument(nameof(transaction));
        var branchIdToCheck = transaction.BranchId;
        var userId = _userHelper.GetCurrentUserId();
        var user = await _userRepository.GetUserWithBranchesAsync(userId, cancellationToken);
        return user.Match<bool>(
            f0: u => u.UserBranches.Any(ub => ub.BranchId == branchIdToCheck), 
            f1: _ =>  false);
    }

  }
}