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
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.Repository;
using CA.ERP.Shared.Dto;
using CA.ERP.Common.Types;

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
            return dalEntity.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var toDelete = await  _context.Set<TDal>().FirstOrDefaultAsync(b => b.Id == id);
            if (toDelete != null)
            {
                toDelete.Status = Status.Inactive;
            }

        }

        public virtual async Task<List<TDomain>> GetManyAsync(int skip = 0, int take = int.MaxValue, Status status = Status.Active, CancellationToken cancellationToken = default)
        {

            IQueryable<TDal> queryable = _context.Set<TDal>().AsQueryable();
            queryable = generateQuery(queryable, status);

            return await queryable.Select(e => _mapper.Map<TDal, TDomain>(e)).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
        }

        private IQueryable<TDal> generateQuery(IQueryable<TDal> queryable, Status status)
        {
            if (status != Status.All)
            {
                queryable = queryable.Where(e => e.Status == status);
            }

            return queryable;
        }

        protected IQueryable<TDal> generateQuery(Status status)
        {
            IQueryable<TDal> queryable = _context.Set<TDal>().AsQueryable();
            if (status != Status.All)
            {
                queryable = queryable.Where(e => e.Status == status);
            }

            return queryable;
        }

        public Task<int> GetCountAsync(Status status = Status.Active, CancellationToken cancellationToken = default)
        {
            IQueryable<TDal> queryable = _context.Set<TDal>().AsQueryable();
            queryable = generateQuery(queryable, status);
            return queryable.CountAsync(cancellationToken);
        }

        public virtual async Task<TDomain> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            TDomain ret = null;

            var queryable = _context.Set<TDal>().AsQueryable().AsNoTracking();

            var entity = await queryable.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
            if (entity != null) {
                ret = _mapper.Map<TDomain>(entity);
            }
            return ret;
        }

        public virtual async Task UpdateAsync(Guid id, TDomain entity, CancellationToken cancellationToken = default)
        {

            var dalEntity = await _context.Set<TDal>().FirstOrDefaultAsync<TDal>(b => b.Id == id, cancellationToken: cancellationToken);
            if (dalEntity != null)
            {
                _mapper.Map(entity, dalEntity);
                dalEntity.Id = id;
                _context.Entry(dalEntity).State = EntityState.Modified;

            }


        }

        public async Task<bool> ExistAsync(Guid id, Status status = Status.Active, CancellationToken cancellationToken = default)
        {

            var queryable = _context.Set<TDal>().AsQueryable();
            if (status != Status.All)
            {
                queryable = queryable.Where(e => e.Status == status);
            }
            return await queryable.AnyAsync(e => e.Id == id, cancellationToken);
        }

        
    }
}
