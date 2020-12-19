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
        Task<OneOf<T, None>> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<OneOf<T, None>> UpdateAsync(string Id, T entity, CancellationToken cancellationToken = default);
        Task<OneOf<Success, None>> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<List<T>> GetAll(CancellationToken cancellationToken = default);
        Task<List<T>> GetAll(int skip, int take, CancellationToken cancellationToken = default);
        Task<OneOf<T, None>> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    }
}
