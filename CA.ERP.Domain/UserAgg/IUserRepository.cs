using CA.ERP.Domain.Base;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.UserAgg
{
    public interface IUserRepository : IRepository<User>
    {

        Task<OneOf<User, None>> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
        Task<OneOf<Success, None>> UpdatePasswordAsync(Guid userId, byte[] passwordHash, byte[] passwordSalt);
        Task<OneOf<User, None>> GetUserWithBranchesAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<int> CountAsync(string username, string firstName, string lastName, UserRole userRole, CancellationToken cancellationToken);
        Task<List<User>> GetManyAsync(string username, string firstName, string lastName, UserRole userRole, CancellationToken cancellationToken, int skip, int take);
        Task UpdateUserRefreshTokenAsync(Guid id, string refreshToken, DateTime expiration, CancellationToken cancellationToken);
        Task<OneOf<User, None>> GetUserByByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    }
}
