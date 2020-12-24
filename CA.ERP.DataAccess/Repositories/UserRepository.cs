using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using CA.ERP.Domain.Helpers;
using Dom = CA.ERP.Domain.UserAgg;
using Dal = CA.ERP.DataAccess.Entities;
using AutoMapper;
using CA.ERP.Domain.UserAgg;
using System.Threading;
using System.Diagnostics.CodeAnalysis;
using OneOf;
using OneOf.Types;
using CA.ERP.Common.Extensions;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.Common;

namespace CA.ERP.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CADataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(CADataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<OneOf<Dom.User, None>> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            OneOf<Dom.User, None> result = null;
            var user = await this._context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user != null) {
                result = _mapper.Map<Dom.User>(user);
            };

            return result;
        }

        public async Task<Guid> AddAsync(Dom.User user, CancellationToken cancellationToken = default)
        {
            user.ThrowIfNullArgument(nameof(user));

            var dalUser = _mapper.Map<Dal.User>(user);
            _context.Entry<Dal.User>(dalUser).State = EntityState.Added;
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
            return user.Id;
        }



        public Task<OneOf<Guid, None>> UpdateAsync(Guid Id, User entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OneOf<Success, None>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAll(int skip = 0, int take = 0, Status status = Status.Active, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OneOf<User, None>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
