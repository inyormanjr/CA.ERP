using CA.ERP.Shared.Dto.Branch;
using CA.ERP.Shared.Dto.User;
using CA.ERP.WebApp.Blazor.Exceptions;
using CA.ERP.WebApp.Blazor.Services;
using Microsoft.Extensions.Logging;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.Management.User
{
    public class UserCreateViewModel : ViewModelBase
    {
        private UserCreate _user;
        private readonly IUserService _userService;
        private readonly IBranchService _branchService;
        private readonly ILogger<UserCreateViewModel> _logger;
        private readonly ISnackbar _snackbar;

        public UserCreate User
        {
            get => _user; set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public List<string> Roles { get; set; } = new List<string>();
        public List<BranchView> Branches { get; set; } = new List<BranchView>();
        public HashSet<BranchView> SelectedBranches { get; set; } = new HashSet<BranchView>();



        public UserCreateViewModel(IUserService userService, IBranchService branchService, ILogger<UserCreateViewModel> logger, ISnackbar snackbar)
        {
            User = new UserCreate();
            _userService = userService;
            _branchService = branchService;
            _logger = logger;
            _snackbar = snackbar;
            Init().ConfigureAwait(false);
        }

        public async Task Init()
        {
            Roles = await _userService.GetRolesAsync();
            OnPropertyChanged(nameof(Roles));

            Branches = (await _branchService.GetBranchesAsync()).Data.ToList();
            OnPropertyChanged(nameof(Branches));
        }

        public async Task Submit()
        {
            try
            {
                User.Branches = SelectedBranches.Select(b => UserBranchCreate.Create(b.Id.ToString(), b.Name)).ToList();

                await _userService.CreateUser(User);

                _snackbar.Add("User Created", Severity.Success);
            }
            catch (ValidationException ex)
            {
                Errors = ex.ValidationErrors.SelectMany(e => e.Value).ToArray();
                _snackbar.Add(ex.Message, Severity.Error);
                OnPropertyChanged(nameof(Errors));
            }
            catch (Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                _logger.LogError("{ex}", ex);
            }
            
        }
    }
}
