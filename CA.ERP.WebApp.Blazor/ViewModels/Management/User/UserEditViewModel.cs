using CA.ERP.Shared.Dto.Branch;
using CA.ERP.Shared.Dto.User;
using CA.ERP.WebApp.Blazor.Exceptions;
using CA.ERP.WebApp.Blazor.Services;
using Microsoft.AspNetCore.Components;
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
        private readonly NavigationManager _navigationManager;

        public string Id { get; private set; }
        public UserUpdate User { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public HashSet<BranchView> Branches { get; set; } = new HashSet<BranchView>();
        public HashSet<BranchView> SelectedBranches { get; set; } = new HashSet<BranchView>();


        public UserEditViewModel(IUserService userService, IBranchService branchService, ILogger<UserEditViewModel> logger, ISnackbar snackbar, NavigationManager navigationManager)
        {
            _userService = userService;
            _branchService = branchService;
            _logger = logger;
            _snackbar = snackbar;
            _navigationManager = navigationManager;
            InitReferences().ConfigureAwait(false);
        }

        public async Task SetUserIdAsync(string id)
        {
            Id = id;
            var userView = await _userService.GetUserAsync(id);
            User = _userService.ConvertUserViewToUserUpdate(userView);
            SelectedBranches = Branches.Where(b => User.Branches.Any(ub => ub.BranchId == b.Id.ToString())).ToHashSet();
            OnPropertyChanged(nameof(User));
            OnPropertyChanged(nameof(SelectedBranches));
            _logger.LogInformation("user branch count {count}", User.Branches.Count);
        }

        public async Task InitReferences()
        {
            var loadBranchTask = _branchService.GetBranchesAsync().ContinueWith(t => {
                Branches = t.Result.Data.ToHashSet();
                SelectedBranches = Branches.Where(b => User.Branches.Any(ub => ub.BranchId == b.Id.ToString())).ToHashSet();
                OnPropertyChanged(nameof(SelectedBranches));
            });

            var loadRolesTask = _userService.GetRolesAsync().ContinueWith(t => Roles = t.Result);

            await Task.WhenAll(loadBranchTask, loadRolesTask);
        }

        public async Task Submit()
        {
            try
            {
                User.Branches = SelectedBranches.Select(b => UserBranchCreate.Create(b.Id.ToString(), b.Name)).ToList();
                await _userService.UpdateUserAsync(Id, User);
                _snackbar.Add("Saved", Severity.Success);
                _navigationManager.NavigateTo($"/management/user/{Id}");
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
