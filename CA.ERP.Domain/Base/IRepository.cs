using CA.ERP.Domain.Common;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.Base
{
    public interface IRepository {
    
    }
    public interface IRepository<T> : IRepository
    {
        Task<Guid> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<OneOf<Guid, None>> UpdateAsync(Guid Id, T entity, CancellationToken cancellationToken = default);
        Task<OneOf<Success, None>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<T>> GetManyAsync(int skip = 0, int take = int.MaxValue, Status status = Status.Active, CancellationToken cancellationToken = default);
        Task<OneOf<T, None>> GetByIdAsync(Guid id, Status status = Status.Active, CancellationToken cancellationToken = default);
        Task<bool> ExistAsync(Guid id, Status status = Status.Active, CancellationToken cancellationToken = default);
    }
}
