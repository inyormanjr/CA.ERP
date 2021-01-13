using CA.ERP.Common.Extensions;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderBranchPermissionValidator : IBranchPermissionValidator<PurchaseOrder>
    {
        private readonly IUserHelper _userHelper;
        private readonly IUserRepository _userRepository;

        public PurchaseOrderBranchPermissionValidator(IUserHelper userHelper, IUserRepository userRepository)
        {
            _userHelper = userHelper;
            _userRepository = userRepository;
        }

        public async Task<bool> HasPermissionAsync(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.ThrowIfNullArgument(nameof(purchaseOrder));
            var branchIdToCheck = purchaseOrder.BranchId;
            var userId = _userHelper.GetCurrentUserId();
            var user = await _userRepository.GetUserWithBranchesAsync(userId);
            return user.Match<bool>(f0: u => u.UserBranches.Any(ub => ub.BranchId == branchIdToCheck), f1: _ => false);
        }
    }
}
