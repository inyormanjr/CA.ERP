using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.PaymentAgg
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<bool> TransactionNumberExistAsync(string transactionNumber, CancellationToken cancellationToken);
        Task<bool> ChequeNumberExistAsync(string chequeNumber, CancellationToken cancellationToken);
    }
}
