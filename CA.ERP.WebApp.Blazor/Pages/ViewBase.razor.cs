using CA.ERP.WebApp.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CA.ERP.WebApp.Blazor.Pages
{
    public partial class ViewBase<T>  : ComponentBase, IDisposable where T : ViewModelBase 
    {
        public T ViewModel { get; protected set; }

        public ViewBase(T viewModel) :base()
        {
            ViewModel = viewModel;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        public void Dispose()
        {
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }




    }
}
