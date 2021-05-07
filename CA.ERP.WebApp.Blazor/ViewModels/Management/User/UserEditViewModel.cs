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
    public class UserEditViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly IBranchService _branchService;
        private readonly ILogger<UserEditViewModel> _logger;
        private readonly ISnackbar _snackbar;

        public string Id { get; private set; }
        public UserUpdate User { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public List<BranchView> Branches { get; set; } = new List<BranchView>();
        public HashSet<BranchView> SelectedBranches { get; set; } = new HashSet<BranchView>();


        public UserEditViewModel(IUserService userService, IBranchService branchService, ILogger<UserEditViewModel> logger, ISnackbar snackbar)
        {
            _userService = userService;
            _branchService = branchService;
            _logger = logger;
            _snackbar = snackbar;

            InitReferences().ConfigureAwait(false);
        }

        public async Task SetUserIdAsync(string id)
        {
            Id = id;
            var userView = await _userService.GetUserAsync(id);
            User = _userService.ConvertUserViewToUserUpdate(userView);
            OnPropertyChanged(nameof(User));
            _logger.LogInformation("user branch count {count}", User.Branches.Count);
        }

        public async Task InitReferences()
        {
            var loadBranchTask = _branchService.GetBranchesAsync().ContinueWith(t => Branches = t.Result.Data.ToList());

            var loadRolesTask = _userService.GetRolesAsync().ContinueWith(t => Roles = t.Result);

            await Task.WhenAll(loadBranchTask, loadRolesTask);
        }

        public async Task Submit()
        {
            try
            {
                await _userService.UpdateUserAsync(Id, User);
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
