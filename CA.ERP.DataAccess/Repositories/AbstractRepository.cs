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
using Dal = CA.ERP.DataAccess.Entities;
using System.Linq.Expressions;

namespace CA.ERP.DataAccess.Repositories
{
    public abstract class AbstractRepository<TDomain, TDal> : IRepository<TDomain> where TDomain : class where TDal : EntityBase
    {
        protected readonly CADataContext _context;
        protected readonly IMapper _mapper;

        public AbstractRepository(CADataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public virtual async Task<Guid> AddAsync(TDomain entity, CancellationToken cancellationToken = default)
        {
            entity.ThrowIfNullArgument(nameof(entity));
            var dalEntity = _mapper.Map<TDomain, TDal>(entity);
            await _context.Set<TDal>().AddAsync(dalEntity, cancellationToken: cancellationToken);
            await _context.SaveChangesAsync();
            return dalEntity.Id;
        }

        public async Task<OneOf<Success, None>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            OneOf<Success, None> ret = default(None);
            var toDelete = _context.Set<TDal>().FirstOrDefault(b => b.Id == id);
            if (toDelete != null)
            {
                toDelete.Status = DataAccess.Common.Status.Inactive;
                await _context.SaveChangesAsync(cancellationToken: cancellationToken);
                ret = default(Success);
            }

            return ret;
        }

        public virtual async Task<List<TDomain>> GetManyAsync(int skip = 0, int take = int.MaxValue, Status status = Status.Active, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Set<TDal>().AsQueryable();
            if (status != Status.All)
            {
                var dalStatus = (DataAccess.Common.Status)status;
                queryable = queryable.Where(e => e.Status == dalStatus);
            }


            return await queryable.Select(e=>_mapper.Map<TDal, TDomain>(e)).ToListAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task<OneOf<TDomain, None>> GetByIdAsync(Guid id, Status status = Status.Active, CancellationToken cancellationToken = default)
        {
            OneOf<TDomain, None> ret = default(None);

            var queryable = _context.Set<TDal>().AsQueryable();
            if (status != Status.All)
            {
                var dalStatus = (DataAccess.Common.Status)status;
                queryable = queryable.Where(e => e.Status == dalStatus);
            }

            var entity = await queryable.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
            if (entity != null) {
                ret = _mapper.Map<TDomain>(entity);
            }
            return ret;
        }

        public virtual async Task<OneOf<Guid, None>> UpdateAsync(Guid id, TDomain entity, CancellationToken cancellationToken = default)
        {
            OneOf<Guid, None> result = default(None);
            var dalEntity = await _context.Set<TDal>().FirstOrDefaultAsync<TDal>(b => b.Id == id, cancellationToken: cancellationToken);
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

        public async Task<bool> ExistAsync(Guid id, Status status = Status.Active, CancellationToken cancellationToken = default)
        {

            var queryable = _context.Set<TDal>().AsQueryable();
            if (status != Status.All)
            {
                var dalStatus = (DataAccess.Common.Status)status;
                queryable = queryable.Where(e => e.Status == dalStatus);
            }
            return await queryable.AnyAsync(e => e.Id == id, cancellationToken);
        }
    }
}
