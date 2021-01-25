using CA.ERP.Common.Extensions;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.PaymentAgg
{
    public class PaymentBranchPermissionValidator : IBranchPermissionValidator<Payment>
    {

        private readonly IUserHelper _userHelper;
        private readonly IUserRepository _userRepository;

        public PaymentBranchPermissionValidator(IUserHelper userHelper, IUserRepository userRepository)
        {
            _userHelper = userHelper;
            _userRepository = userRepository;
        }

        public async Task<bool> HasPermissionAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            payment.ThrowIfNullArgument(nameof(payment));
            var branchIdToCheck = payment.BranchId;
            var userId = _userHelper.GetCurrentUserId();
            var user = await _userRepository.GetUserWithBranchesAsync(userId, cancellationToken);
            return user.Match<bool>(f0: u => u.UserBranches.Any(ub => ub.BranchId == branchIdToCheck), f1: _ => false);
        }
    }
}
