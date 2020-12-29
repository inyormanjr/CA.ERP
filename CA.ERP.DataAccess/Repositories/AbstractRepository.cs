using CA.ERP.Domain.Base;
using CA.ERP.Domain.Common;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CA.ERP.Common.Extensions;
using AutoMapper;
using CA.ERP.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CA.ERP.DataAccess.Repositories
{
    public abstract class AbstractRepository<T1, T2> : IRepository<T1> where T1 : class where T2 : EntityBase
    {
        protected readonly CADataContext _context;
        protected readonly IMapper _mapper;

        public AbstractRepository(CADataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Guid> AddAsync(T1 entity, CancellationToken cancellationToken = default)
        {
            entity.ThrowIfNullArgument(nameof(entity));
            var dalEntity = _mapper.Map<T2>(entity);
            await _context.Set<T2>().AddAsync(dalEntity, cancellationToken: cancellationToken);
            await _context.SaveChangesAsync();
            return dalEntity.Id;
        }

        public async Task<OneOf<Success, None>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            OneOf<Success, None> ret = default(None);
            var toDelete = _context.Set<T2>().FirstOrDefault(b => b.Id == id);
            if (toDelete != null)
            {
                toDelete.Status = DataAccess.Common.Status.Inactive;
                await _context.SaveChangesAsync(cancellationToken: cancellationToken);
                ret = default(Success);
            }

            return ret;
        }

        public async Task<List<T1>> GetManyAsync(int skip = 0, int take = int.MaxValue, Status status = Status.Active, CancellationToken cancellationToken = default)
        {
            var entities = await _context.Set<T2>().ToListAsync(cancellationToken: cancellationToken);
            return _mapper.Map<List<T1>>(entities);
        }

        public async Task<OneOf<T1, None>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            OneOf<T1, None> ret = default(None);
            var entity = await _context.Set<T2>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
            if (entity != null) {
                ret = _mapper.Map<T1>(entity);
            }
            return ret;
        }

        public virtual async Task<OneOf<Guid, None>> UpdateAsync(Guid id, T1 entity, CancellationToken cancellationToken = default)
        {
            OneOf<Guid, None> result = default(None);
            var dalEntity = await _context.Set<T2>().FirstOrDefaultAsync<T2>(b => b.Id == id, cancellationToken: cancellationToken);
            if (dalEntity != null)
            {
                _mapper.Map(entity, dalEntity);
                dalEntity.Id = id;
                _context.Entry(dalEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync(cancellationToken: cancellationToken);
                result = dalEntity.Id;
            }

            return result;
        }
    }
}
