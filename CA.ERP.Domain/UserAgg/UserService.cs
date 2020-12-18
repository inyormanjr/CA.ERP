using CA.ERP.Domain.Base;
using CA.ERP.Domain.Helpers;
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

        public async Task<string> AddUserAsync(string username, string password, int branchId)
        {
            var user = _userFactory.CreateUser(username, password, branchId);
            _passwordManagementHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.SetHashAndSalt(passwordHash, passwordSalt);
            user = await _userRepository.Add(user);
            return user.Id;
        }

        public async Task<string> AuthenticateUser(string username, string password, CancellationToken cancellationToken = default)
        {
            string userId = null;
            var user = await _userRepository.GetUserByUsernameAsync(username, cancellationToken);
            if (user != null && _passwordManagementHelper.VerifyPasswordhash(password, user.PasswordHash, user.PasswordSalt))
            {
                userId = user.Id.ToString();
            }
            return userId;
        }
    }
}
