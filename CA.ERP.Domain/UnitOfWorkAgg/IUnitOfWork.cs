using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.UnitOfWorkAgg
{
    public interface IUnitOfWork : IDisposable
    {
        public Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
