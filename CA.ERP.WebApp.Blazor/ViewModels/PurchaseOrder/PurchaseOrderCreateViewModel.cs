using CA.ERP.Shared.Dto.Branch;
using CA.ERP.Shared.Dto.MasterProduct;
using CA.ERP.Shared.Dto.PurchaseOrder;
using CA.ERP.Shared.Dto.Supplier;
using CA.ERP.WebApp.Blazor.Exceptions;
using CA.ERP.WebApp.Blazor.Services;
using Microsoft.Extensions.Logging;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.ViewModels.PurchaseOrder
{

    public class PurchaseOrderCreateViewModel : ViewModelBase
    {
        private readonly ILogger<PurchaseOrderCreateViewModel> _logger;
        private readonly ISnackbar _snackbar;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly BranchService _branchService;
        private readonly SupplierService _supplierService;
        private readonly MasterProductService _masterProductService;
        private List<BranchView> _branches = new List<BranchView>();
        private bool _branchesIsLoading = false;
        private SupplierView _selectedSupplier;
        private List<SupplierView> _suppliers = new List<SupplierView>();
        private BranchView _branch;
        private List<SupplierBrandView> _supplierBrands = new List<SupplierBrandView>();
        private SupplierBrandView _selectedSupplierBrand;
        private List<MasterProductView> _masterProducts = new List<MasterProductView>();
        private MasterProductView _selectedMasterProduct;
        private DateTime? _deliveryDate;
        private List<string> _errors;

        public List<BranchView> Branches
        {
            get => _branches; set
            {
                _branches = value;
                OnPropertyChanged("Branches");
            }
        }

        public BranchView SelectedBranch
        {
            get => _branch; set
            {
                _branch = value;
                OnPropertyChanged("Branch");
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

        public SupplierView SelectedSupplier
        {
            get => _selectedSupplier; set
            {
                _selectedSupplier = value;
                LoadSupplierBrands().ConfigureAwait(false);
                SelectedSupplierBrand = null;
                OnPropertyChanged("Supplier");
                
            }
        }

        public List<SupplierView> Suppliers
        {
            get => _suppliers;
            set
            {
                _suppliers = value;
                OnPropertyChanged("Suppliers");
            }

        }

        public List<SupplierBrandView> SupplierBrands
        {
            get => _supplierBrands; set
            {
                _supplierBrands = value;
                OnPropertyChanged("SupplierBrands");
            }
        }

        public SupplierBrandView SelectedSupplierBrand
        {
            get => _selectedSupplierBrand; set
            {
                _selectedSupplierBrand = value;
                SelectedMasterProduct = null;
                if (_selectedSupplierBrand != null)
                {
                    LoadMasterProducts().ConfigureAwait(false);

                }


                OnPropertyChanged("SelectedSupplierBrand");
            }
        }


        public List<MasterProductView> MasterProducts
        {
            get => _masterProducts; set
            {
                _masterProducts = value;
                OnPropertyChanged("MasterProducts");
            }
        }

        public MasterProductView SelectedMasterProduct
        {
            get => _selectedMasterProduct; set
            {
                _selectedMasterProduct = value;
                OnPropertyChanged("SelectedMasterProduct");
            }
        }

        public PurchaseOrderCreate PurchaseOrderCreate { get; set; } = new PurchaseOrderCreate();

        public DateTime? DeliveryDate
        {
            get => _deliveryDate; set
            {
                _deliveryDate = value;
                OnPropertyChanged("DeliveryDate");
            }
        }

        public bool CanSave
        {
            get
            {
                return DeliveryDate != null && SelectedBranch != null && PurchaseOrderCreate.PurchaseOrderItems.Count > 0;
            }
        }

        public List<string> Errors
        {
            get => _errors; set
            {
                _errors = value;
                OnPropertyChanged("Errors");
            }
        }

        public bool IsSaving { get; set; }

        public PurchaseOrderCreateViewModel(ILogger<PurchaseOrderCreateViewModel> logger, ISnackbar snackbar, IPurchaseOrderService purchaseOrderService, BranchService branchService, SupplierService supplierService, MasterProductService masterProductService)
        {
            _logger = logger;
            _snackbar = snackbar;
            _purchaseOrderService = purchaseOrderService;
            _branchService = branchService;
            _supplierService = supplierService;
            _masterProductService = masterProductService;
            Init().ConfigureAwait(false);


        }

        public void AddPurchaseOrderItem()
        {
            if (SelectedMasterProduct != null && SelectedSupplierBrand != null)
            {
                var purchaseOrderItem = new PurchaseOrderItemCreate()
                {
                    BrandName = SelectedSupplierBrand.BrandName,
                    Model = SelectedMasterProduct.Model,
                    MasterProductId = SelectedMasterProduct.Id

                };

                PurchaseOrderCreate.PurchaseOrderItems.Add(purchaseOrderItem);
                SelectedMasterProduct = null;
                OnPropertyChanged("PurchaseOrderCreate");
            }

        }

        public void RemovePurchaseOrderItem(PurchaseOrderItemCreate purchaseOrderItem)
        {
            PurchaseOrderCreate.PurchaseOrderItems.Remove(purchaseOrderItem);
            OnPropertyChanged("PurchaseOrderCreate");
        }



        public async Task SaveAsync()
        {
            if (CanSave)
            {
                try
                {
                    IsSaving = true;
                    OnPropertyChanged("IsSaving");

                    PurchaseOrderCreate.DeliveryDate = DateTime.SpecifyKind(DeliveryDate.Value, DateTimeKind.Local);
                    PurchaseOrderCreate.DestinationBranchId = SelectedBranch.Id;
                    PurchaseOrderCreate.SupplierId = SelectedSupplier.Id;

                    var id = await _purchaseOrderService.CreatePurchaseOrderAsync(PurchaseOrderCreate);
                    _snackbar.Add("Saving successful");
                }
                catch (ValidationException ex)
                {
                    _snackbar.Add(ex.Message);
                    Errors = ex.ValidationErrors.SelectMany(ve => ve.Value).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error saving purchase order", ex);
                    _snackbar.Add(ex.Message, Severity.Error);
                }
                finally
                {
                    IsSaving = false;
                    OnPropertyChanged("IsSaving");
                }
            }
        }

        public async Task Init()
        {
            var loadBranchsTask = LoadBranches();


            var loadSuppliersTask = LoadSuppliers();



            await Task.WhenAll(loadBranchsTask, loadSuppliersTask);
        }

        private async Task LoadSuppliers()
        {
            //load all supplier to memory
            var paginatedSuppliers = await _supplierService.GetSuppliersAsync(null, 0, 99999);
            _logger.LogDebug($"Paginated count : {paginatedSuppliers.Data.Count().ToString()}");
            Suppliers = paginatedSuppliers.Data.ToList();
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
                    _logger.LogDebug($"Branch is loading :{BranchesIsLoading}");
                }

                BranchesIsLoading = false;
            });
        }

        private async Task LoadSupplierBrands()
        {
            _logger.LogDebug("Loading  Brands");
            if (SelectedSupplier != null)
            {
                SupplierBrands = await _supplierService.GetSupplierBrandsAsync(SelectedSupplier.Id);
                _logger.LogDebug("Supplier Brands Count {SupllierBrandCount}", SupplierBrands.Count);
            }

        }

        private async Task LoadMasterProducts()
        {
            _logger.LogDebug("Loading  MasterProducts");
            if (SelectedSupplierBrand != null)
            {
                MasterProducts = await _masterProductService.GetMasterProductsWithBrandAndSupplier(SelectedSupplierBrand.BrandId, SelectedSupplierBrand.SupplierId);
                _logger.LogDebug("MasterProducts Count {MasterProductsCount}", MasterProducts.Count);
            }

        }



        public Task<IEnumerable<SupplierView>> SearchSuppliers(string name)
        {
            var suppliers = Suppliers.Where(s => s.Name.StartsWith(name ?? "", StringComparison.OrdinalIgnoreCase)).OrderBy(s => s.Name).ToList();
            
            return Task.FromResult<IEnumerable<SupplierView>>(suppliers);
        }

        public Task<IEnumerable<BranchView>> SearchBranches(string name)
        {
            var branches = Branches.Where(s => s.Name.Contains(name ?? "", StringComparison.OrdinalIgnoreCase)).OrderBy(s => s.Name).ToList();
            return Task.FromResult<IEnumerable<BranchView>>(branches);
        }

        public Task<IEnumerable<SupplierBrandView>> SearchSupplierBrand(string brandName)
        {
            var supplierBrands = SupplierBrands.Where(s => s.BrandName.Contains(brandName ?? "", StringComparison.OrdinalIgnoreCase)).OrderBy(s => s.BrandName).ToList();
            return Task.FromResult<IEnumerable<SupplierBrandView>>(supplierBrands);

        }

        public Task<IEnumerable<MasterProductView>> SearchMasterProducts(string model)
        {
            var masterProducts = MasterProducts.Where(s => s.Model.Contains(model ?? "", StringComparison.OrdinalIgnoreCase)).OrderBy(s => s.Model).ToList();
            return Task.FromResult<IEnumerable<MasterProductView>>(masterProducts);
        }
    }
}
