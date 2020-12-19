using CA.ERP.Domain.Base;
using CA.ERP.Domain.Helpers;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.UserAgg
{
    public class UserService : ServiceBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;
        private readonly PasswordManagementHelper _passwordManagementHelper;

        public UserService(IUserRepository userRepository, IUserFactory userFactory, PasswordManagementHelper passwordManagementHelper)
        {
            _userRepository = userRepository;
            _userFactory = userFactory;
            _passwordManagementHelper = passwordManagementHelper;
        }

        public async Task<OneOf<string, Error>> CreateUserAsync(string username, string password, int branchId, CancellationToken cancellationToken)
        {
            var user = _userFactory.CreateUser(username, password, branchId);
            _passwordManagementHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.SetHashAndSalt(passwordHash, passwordSalt);
            var result = await _userRepository.AddAsync(user, cancellationToken: cancellationToken);
            return result.Match<OneOf<string, Error>>(
                f0: (u) => u.Id,
                f1: (none) => default(Error)
                );
        }

        public async Task<OneOf<string, None>> AuthenticateUser(string username, string password, CancellationToken cancellationToken = default)
        {
            
            var optionUser = await _userRepository.GetUserByUsernameAsync(username, cancellationToken);
            return optionUser.MapT0(user => _passwordManagementHelper.VerifyPasswordhash(password, user.PasswordHash, user.PasswordSalt) ? user.Id : null);

        }
    }
}
