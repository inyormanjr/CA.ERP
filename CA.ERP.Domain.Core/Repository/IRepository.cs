using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.Core.Repository
{
    public interface IRepository
    {

    }
    public interface IRepository<T> : IRepository
    {
        Task<Guid> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid Id, T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<T>> GetManyAsync(int skip = 0, int take = int.MaxValue, Status status = Status.Active, CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(Status status = Status.Active, CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistAsync(Guid id, Status status = Status.Active, CancellationToken cancellationToken = default);

    }
}
