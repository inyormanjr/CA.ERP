using AutoMapper;
using CA.ERP.Domain.PaymentAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Repositories
{
    public class PaymentRepository : AbstractRepository<Payment, Entities.Payment>, IPaymentRepository
    {
        public PaymentRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<bool> ChequeNumberExistAsync(string chequeNumber, CancellationToken cancellationToken)
        {
            return await _context.ChequePaymentDetails.AnyAsync(cp => cp.ChequeNumber == chequeNumber, cancellationToken);
        }

        public async Task<bool> TransactionNumberExistAsync(string transactionNumber, CancellationToken cancellationToken)
        {
            return await _context.CardPaymentDetails.AnyAsync(cp => cp.TransactionNumber == transactionNumber, cancellationToken);
        }
    }
}
