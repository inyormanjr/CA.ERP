using CA.ERP.Shared.Dto.Branch;
using CA.ERP.WebApp.Blazor.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.PurchaseOrder
{

    public class PurchaseOrderCreateViewModel : ViewModelBase
    {
        private readonly ILogger<PurchaseOrderCreateViewModel> _logger;
        private readonly BranchService _branchService;
        private List<BranchView> _branches = new List<BranchView>();
        private bool _branchesIsLoading = false;

        public List<BranchView> Branches
        {
            get => _branches; set
            {
                _branches = value;
                OnPropertyChanged("Branches");
            }
        }

        public bool BranchesIsLoading
        {
            get => _branchesIsLoading; set
            {
                _branchesIsLoading = value;
                OnPropertyChanged("BranchesIsLoading");
            }
        }
        public PurchaseOrderCreateViewModel(ILogger<PurchaseOrderCreateViewModel>  logger, BranchService branchService)
        {
            _logger = logger;
            _branchService = branchService;
            Init().ConfigureAwait(false);
            
        }

        public async Task Init()
        {
            var loadBranchTask = LoadBranches();

            await Task.WhenAll(loadBranchTask);
        }

        private async Task LoadBranches()
        {
            BranchesIsLoading = true;
            await _branchService.GetBranchesAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    ErrorMessage = t.Exception.Message;
                }
                else
                {
                    
                    Branches = t.Result.Data.ToList();
                    _logger.LogInformation($"Branch is loading :{BranchesIsLoading}");
                }

                BranchesIsLoading = false;
            });
        }
    }
}
