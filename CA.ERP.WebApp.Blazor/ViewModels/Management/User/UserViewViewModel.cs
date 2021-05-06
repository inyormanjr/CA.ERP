using CA.ERP.Shared.Dto.User;
using CA.ERP.WebApp.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.Management.User
{
    public class UserViewViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private string _id;

        public UserView User { get; private set; }
        public string Id
        {
            get => _id; set
            {
                _id = value;
                loadUser().ConfigureAwait(false);
            }
        }

        public UserViewViewModel(IUserService userService)
        {
            _userService = userService;


        }

        

        private async Task loadUser()
        {
            if (Id != null)
            {
                User = await _userService.GetUserAsync(Id);
                OnPropertyChanged(nameof(User));
            }
        }
    }
}
