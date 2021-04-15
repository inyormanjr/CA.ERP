using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.User;
using CA.ERP.WebApp.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.Management.User
{
    public class UserListViewModel : ViewModelBase
    {
        private readonly IUserService _userService;

        public string FirsName { get; set; }
        public string LastName { get; set; }

        public UserListViewModel(IUserService userService)
        {
            _userService = userService;
        }

        public Task<PaginatedResponse<UserView>> GetUsersAsync(int page, int size)
        {
            return _userService.GetUsersAsync(FirsName, LastName, page, size);
        }
    }
}
