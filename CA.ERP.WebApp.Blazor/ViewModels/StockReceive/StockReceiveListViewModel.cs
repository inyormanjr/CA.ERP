using CA.ERP.Common.Types;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.Branch;
using CA.ERP.Shared.Dto.StockReceive;
using CA.ERP.Shared.Dto.Supplier;
using CA.ERP.WebApp.Blazor.Services;
using CA.ERP.WebApp.Blazor.ViewModels.Mixins;
using Microsoft.Toolkit.Mvvm.Input;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CA.ERP.WebApp.Blazor.ViewModels.StockReceive
{
    public class StockReceiveListViewModel : ViewModelBase, ISelectBranchMixin
    {
        private readonly IStockReceiveService _stockReceiveService;
        private readonly IBranchService _branchService;
        private readonly ISnackbar _snackbar;

        public List<BranchView> Branches { get; set; }


        public BranchView SelectedBranch { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateReceive { get; set; }

        public StockSource? Source { get; set; }

        public StockReceiveStage? Stage { get; set; }

        

        public StockReceiveListViewModel(IStockReceiveService stockReceiveService, IBranchService branchService, ISnackbar snackbar)
        {
            _stockReceiveService = stockReceiveService;
            _branchService = branchService;
            _snackbar = snackbar;


            Init().ConfigureAwait(false);
        }

        public async Task Init()
        {
            var loadBranchsTask = LoadBranches();
            await Task.WhenAll(loadBranchsTask);
        }



        private async Task LoadBranches()
        {
            
            var pagindatedData = await _branchService.GetBranchesAsync();
            Branches = pagindatedData.Data.ToList();
            OnPropertyChanged(nameof(Branches));
        }


        public Task<IEnumerable<BranchView>> SearchBranches(string name)
        {
            var branches = Branches.Where(s => s.Name.Contains(name ?? "", StringComparison.OrdinalIgnoreCase)).OrderBy(s => s.Name).ToList();
            return Task.FromResult<IEnumerable<BranchView>>(branches);
        }


        public Task<PaginatedResponse<StockReceiveView>> SearchStockReceivesAsync(int page, int size) {
            var ret = new PaginatedResponse<StockReceiveView>();
            try
            {
                return _stockReceiveService.GetStockReceivesAsync(SelectedBranch?.Id, DateCreated, DateReceive, Source, Stage, page, size);
            }
            catch (Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                ErrorMessage = ex.Message;

            }

            return Task.FromResult(ret);
        }
    }
}
