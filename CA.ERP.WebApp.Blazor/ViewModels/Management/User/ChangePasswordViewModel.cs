using CA.ERP.Shared.Dto.User;
using CA.ERP.WebApp.Blazor.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.Management.User
{
    public class ChangePasswordViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly NavigationManager _navigationManager;

        public UserChangePassword PasswordUpdate { get; set; } = new UserChangePassword();
        public string UserId { get; set; }
        public bool IsSaving { get; set; }

        public ChangePasswordViewModel(IUserService userService, NavigationManager navigationManager)
        {
            _userService = userService;
            _navigationManager = navigationManager;
        }

        public async Task Submit()
        {
            try
            {
                IsSaving = true;
                OnPropertyChanged(nameof(IsSaving));
                await _userService.ChangePasswordAsync(UserId, PasswordUpdate);
                _navigationManager.NavigateTo("/management/user");
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                IsSaving = false;
                OnPropertyChanged(nameof(IsSaving));
            }
            
        }
    }
}
