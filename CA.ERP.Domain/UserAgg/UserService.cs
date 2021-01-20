using CA.ERP.Domain.Base;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Helpers;
using CA.ERP.Domain.UnitOfWorkAgg;
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
    public class UserService : ServiceBase<User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IUserFactory _userFactory;
        private readonly PasswordManagementHelper _passwordManagementHelper;
        private readonly IValidator<User> _userValidator;

        public UserService(IUnitOfWork unitOfWork,IUserRepository userRepository, IBranchRepository branchRepository, IUserFactory userFactory, PasswordManagementHelper passwordManagementHelper, IValidator<User> userValidator, IUserHelper userHelper)
            : base(unitOfWork, userRepository, userValidator, userHelper)
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
            List<ValidationFailure> validationFailures = new List<ValidationFailure>();
            var notFoundBranches = branchIds.Except(branches.Select(b => b.Id));
            if (notFoundBranches.Any())
            {
                var branchNames = branches.Where(b => notFoundBranches.Contains(b.Id)).Select(b => b.Name);
                validationFailures.Add(new ValidationFailure("UserBranches", $"Invalid branches : {string.Join(", ", branchNames)}"));
            }

            if (validationFailures.Any())
            {
                ret = validationFailures;
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
                    user.CreatedBy = _userHelper.GetCurrentUserId();
                    user.UpdatedBy = _userHelper.GetCurrentUserId();
                    ret = await _userRepository.AddAsync(user, cancellationToken: cancellationToken);
                }
            }
            await _unitOfWork.CommitAsync();
            return ret;

            
        }

        public async Task<OneOf<Guid, List<ValidationFailure>, NotFound>> UpdateUser(Guid id, User user, List<Guid> branchIds, CancellationToken cancellationToken)
        {
            //init result
            OneOf<Guid, List<ValidationFailure>, NotFound> ret = new List<ValidationFailure>();
            //get branches
            List<Branch> branches = await _branchRepository.GetBranchsAsync(branchIds, cancellationToken: cancellationToken);

            //validation failure

            List<ValidationFailure> validationFailures = new List<ValidationFailure>();
            //validate all branch id are present
            var notFoundBranches = branchIds.Except(branches.Select(b => b.Id));
            if (notFoundBranches.Any())
            {
                var branchNames = branches.Where(b => notFoundBranches.Contains(b.Id)).Select(b => b.Name);
                validationFailures.Add(new ValidationFailure("UserBranches", $"Invalid branches : {string.Join(", ", branchNames)}"));
            }

            if (validationFailures.Any())
            {
                ret = validationFailures;
            }
            else
            {
                //validate user;
                var validationResult = _userValidator.Validate(user);
                if (!validationResult.IsValid)
                {
                    ret = validationResult.Errors.ToList();
                }
                else
                {
                    user.UserBranches.Clear();

                    foreach (var branch in branches)
                    {
                        user.UserBranches.Add(new UserBranch() { BranchId = branch.Id });
                    }

                    var userOption = await _userRepository.UpdateAsync(id ,user, cancellationToken: cancellationToken);
                    ret = userOption.Match<OneOf<Guid, List<ValidationFailure>, NotFound>>(
                        f0: id => id,
                        f1: none => default(NotFound)
                    );
                    await _unitOfWork.CommitAsync();
                }
            }
            return ret;
        }

        public async Task<OneOf<Success, List<ValidationFailure>, NotFound>> UpdateUserPassword(Guid id, string password, string confirmPassword, CancellationToken cancellationToken)
        {
            //init result
            OneOf<Success, List<ValidationFailure>, NotFound> ret = new List<ValidationFailure>();
            var validationFailures = new List<ValidationFailure>();
            if (string.IsNullOrEmpty(password))
            {
                validationFailures.Add(new ValidationFailure("Password", "Password is required."));
            }
            if(string.IsNullOrEmpty(confirmPassword))
            {
                validationFailures.Add(new ValidationFailure("ConfirmPassword", "ConfirmPassword is required."));
            }
            if (confirmPassword != password)
            {
                validationFailures.Add(new ValidationFailure("Password", "Password and ConfirmPassword must match."));

            }
            if (validationFailures.Any())
            {
                ret = validationFailures;
            }
            else
            {
                _passwordManagementHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                var passwordOption = await _userRepository.UpdatePasswordAsync(id,passwordHash, passwordSalt);
                ret = passwordOption.Match<OneOf<Success, List<ValidationFailure> , NotFound>>(
                    f0: success => success,
                    f1: none => default(NotFound)
                    ); ;

                await _unitOfWork.CommitAsync();
            }
            return ret;
        }

        public async Task<PaginationBase<User>> GetManyAsync(string username, string firstName, string lastName, UserRole userRole, int pageSize = 10, int currentPage = 1, CancellationToken cancellationToken = default)
        {;
            int count =  await _userRepository.CountAsync(username, firstName, lastName, userRole, cancellationToken);

            int skip = (currentPage - 1) * pageSize;
            int take = pageSize;

            var users = await  _userRepository.GetManyAsync(username, firstName, lastName, userRole, cancellationToken, skip, take);
            double totalPages = (double)count / (double)pageSize;
            return new PaginatedUsers()
            {
                Data = users.ToList(),
                CurrentPage = currentPage,
                TotalPage = (int)Math.Ceiling(totalPages),
                PageSize = pageSize,
                TotalCount = count,
            };
        }

        public async override Task<OneOf<User, NotFound>> GetOneAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var userResult = await _userRepository.GetUserWithBranchesAsync(id, cancellationToken);
            return userResult.Match<OneOf<User, NotFound>>(f0: user => user, f1: _ => default(NotFound));
        }


        public async Task<OneOf<User, None>> AuthenticateUser(string username, string password, CancellationToken cancellationToken = default)
        {
            OneOf<User, None> ret = default(None);
            //manual validation
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var optionUser = await _userRepository.GetUserByUsernameAsync(username, cancellationToken);
                return optionUser.Match<OneOf<User, None>>(
                    f0: user => {
                        OneOf<User, None> ret = default(None);
                        if (_passwordManagementHelper.VerifyPasswordhash(password, user.PasswordHash, user.PasswordSalt))
                        {
                            ret = user;
                        }
                        return ret;
                    },
                    f1: none => default(None)
                );
            }
            return ret;
        }
    }
}
