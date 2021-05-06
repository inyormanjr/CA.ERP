using CA.ERP.Shared.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.Management.User
{
    public class UserEditViewModel : ViewModelBase
    {
        public UserUpdate User { get; set; }
        public string Id { get; set; }
    }
}
