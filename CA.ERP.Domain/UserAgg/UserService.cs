using CA.ERP.Domain.Base;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Helpers;
using FluentValidation;
using FluentValidation.Results;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.UserAgg
{
    public class UserService : ServiceBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IUserFactory _userFactory;
        private readonly PasswordManagementHelper _passwordManagementHelper;
        private readonly IValidator<User> _userValidator;

        public UserService(IUserRepository userRepository, IBranchRepository branchRepository, IUserFactory userFactory, PasswordManagementHelper passwordManagementHelper, IValidator<User> userValidator )
        {
            _userRepository = userRepository;
            _branchRepository = branchRepository;
            _userFactory = userFactory;
            _passwordManagementHelper = passwordManagementHelper;
            _userValidator = userValidator;
        }

        public async Task<OneOf<Guid, List<ValidationFailure>, Error<string>>> CreateUserAsync(string username, string password, UserRole role, string firstName, string lastName, List<Guid> branchIds, CancellationToken cancellationToken)
        {
            //init result
            OneOf<Guid, List<ValidationFailure>, Error<string>> ret = new List<ValidationFailure>();
            //get branches
            List<Branch> branches = await _branchRepository.GetBranchsAsync(branchIds, cancellationToken: cancellationToken);

            //validate all branch id are present
            var notFoundBranches = branchIds.Except(branches.Select(b => b.Id));
            if (notFoundBranches.Any())
            {
                ret = new Error<string>("One or more branch is invalid.");
            }
            else
            {
                var user = _userFactory.CreateUser(username, role, firstName, lastName);

                foreach (var branch in branches)
                {
                    user.UserBranches.Add(new UserBranch() { BranchId = branch.Id });
                }

                _passwordManagementHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                user.SetHashAndSalt(passwordHash, passwordSalt);

                //validate user;
                var validationResult = _userValidator.Validate(user);
                if (!validationResult.IsValid)
                {
                    ret = validationResult.Errors.ToList();
                }
                else
                {
                    ret = await _userRepository.AddAsync(user, cancellationToken: cancellationToken);
                }
            }
            return ret;

            
        }

        public async Task<OneOf<User, None>> AuthenticateUser(string username, string password, CancellationToken cancellationToken = default)
        {
            
            var optionUser = await _userRepository.GetUserByUsernameAsync(username, cancellationToken);
            return optionUser.Match(
                f0: user => _passwordManagementHelper.VerifyPasswordhash(password, user.PasswordHash, user.PasswordSalt) ? user : null,
                f1 : none => null
            );

        }
    }
}
