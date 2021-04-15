using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels
{
    public class ViewModelBase : ObservableRecipient
    {
        public string ErrorMessage { get; set; }
        public string[] Errors { get; set; }
    }
}
