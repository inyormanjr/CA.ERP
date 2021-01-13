using CA.ERP.Common.Extensions;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockReceiveAgg
{
    public class StockReceiveBranchPermissionValidator : IBranchPermissionValidator<StockReceive>
    {
        private readonly IUserHelper _userHelper;
        private readonly IUserRepository _userRepository;

        public StockReceiveBranchPermissionValidator(IUserHelper userHelper, IUserRepository userRepository)
        {
            _userHelper = userHelper;
            _userRepository = userRepository;
        }
        public async Task<bool> HasPermissionAsync(StockReceive stockReceive)
        {
            stockReceive.ThrowIfNullArgument(nameof(stockReceive));
            var branchIdToCheck = stockReceive.BranchId;
            var userId = _userHelper.GetCurrentUserId();
            var user = await _userRepository.GetUserWithBranchesAsync(userId);
            return user.Match<bool>(f0: u => u.UserBranches.Any(ub => ub.BranchId == branchIdToCheck), f1: _ => false);
        }
    }
}
