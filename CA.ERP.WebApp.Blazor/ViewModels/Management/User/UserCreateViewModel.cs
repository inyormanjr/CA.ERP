using CA.ERP.Shared.Dto.Branch;
using CA.ERP.Shared.Dto.User;
using CA.ERP.WebApp.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.Management.User
{
    public class UserCreateViewModel : ViewModelBase
    {
        private UserCreate _user;
        private readonly UserService _userService;
        private readonly IBranchService _branchService;

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



        public UserCreateViewModel(UserService userService, IBranchService branchService)
        {
            User = new UserCreate();
            _userService = userService;
            _branchService = branchService;
            Init().ConfigureAwait(false);
        }

        public async Task Init()
        {
            Roles = await _userService.GetRolesAsync();
            OnPropertyChanged(nameof(Roles));

            Branches = (await _branchService.GetBranchesAsync()).Data.ToList();
            OnPropertyChanged(nameof(Branches));
        }
    }
}
