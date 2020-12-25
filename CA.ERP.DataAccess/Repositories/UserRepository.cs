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
    public class UserRepository :AbstractRepository<User, Dal.User>, IUserRepository
    {

        public UserRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {

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


    }
}
