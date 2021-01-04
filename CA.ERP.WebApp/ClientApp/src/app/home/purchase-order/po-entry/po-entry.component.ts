import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { BranchService } from '../../management/branch/branch.service';
import { BranchView } from '../../management/branch/model/branch.view';
import { SupplierService } from '../supplier/services/supplier.service';
import { Location } from '@angular/common';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { SupplierSelectionModalComponent } from '../supplier/supplier-selection-modal/supplier-selection-modal.component';
import { SupplierView } from '../supplier/models/supplier-view';
import { PurchaseOrderState } from '../reducers';
import { Store } from '@ngrx/store';
import { PurchaseOrderSelectorType } from '../reducers/purchase-order.selector.type';
import { BrandWithMasterProducts } from '../supplier/models/brandWithMasterProducts';
import { MasterProduct } from '../supplier/models/masterProduct';
import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { PurchaseOrderService } from '../purchase-order.service';
import { map } from 'rxjs/operators';
import { NewRequest } from 'src/app/models/NewRequest';
import { Router } from '@angular/router';
import { identifierModuleUrl } from '@angular/compiler';
@Component({
  selector: 'app-po-entry',
  templateUrl: './po-entry.component.html',
  styleUrls: ['./po-entry.component.css'],
})
export class PoEntryComponent implements OnInit {
  branches$: Observable<BranchView[]>;
  selectedSupplier$: Observable<SupplierView>;
  brands$: Observable<BrandWithMasterProducts[]>;
  poForm: FormGroup;
  selectedSupplier: SupplierView;
  selectedBrand: BrandWithMasterProducts;
  selectedModel: MasterProduct;
  bsModalRef: BsModalRef;
  modalConfig = {
    backdrop: true,
    ignoreBackdropClick: false,
  };
  constructor(
    private branchService: BranchService,
    private fb: FormBuilder,
    private modalSerivce: BsModalService,
    private purchaseOrderStore: Store<PurchaseOrderState>,
    private ref: ChangeDetectorRef,
    private alertifyService: AlertifyService,
    private poservice: PurchaseOrderService,
    private route: Router
  ) {
    this.selectedSupplier$ = this.purchaseOrderStore.select(
      PurchaseOrderSelectorType.selectedSupplier
    );
    this.brands$ = this.purchaseOrderStore.select(
      PurchaseOrderSelectorType.selectBrandsWithModels
    );
    this.branches$ = this.branchService.get();
    this.initializePOFormGroup();
  }

  initializePOFormGroup() {
     this.poForm = this.fb.group({
       supplierId: [this.selectedSupplier, [Validators.required]],
       branchId: [0, [Validators.required]],
       deliveryDate: [Date.now(), [Validators.required]],
       purchaseOrderItems: this.fb.array([]),
     });
  }

  trackByFn(index: any, item: any) {
    return index;
  }

  selectSupplierModal() {
    this.bsModalRef = this.modalSerivce.show(
      SupplierSelectionModalComponent,
      this.modalConfig
    );
  }

  onSelectBrand(event: BrandWithMasterProducts) {
    this.selectedBrand = event;
  }
  get createDetails(): FormGroup {
    return this.fb.group({
      masterProductId: [
        this.selectedModel.masterProductId,
        Validators.required,
      ],
      brandName: [this.selectedBrand.brandName],
      masterProductName: [this.selectedModel.model, Validators.required],
      orderedQuantity: [0, Validators.required],
      freeQuantity: [0, Validators.required],
      totalQuantity: [0, Validators.required],
      costPrice: [this.selectedModel.costPrice, Validators.required],
      discount: [0, Validators.required],
      totalCostPrice: [0, Validators.required],
      deliveryQuantity: [0, Validators.required],
    });
  }

   selectedPoDetals(index): FormGroup {
    return (this.poForm.controls.purchaseOrderItems as FormArray)[index];
  }

  get purchaseOrderItemsFormArray(): FormArray {
    return this.poForm.controls.purchaseOrderItems as FormArray;
  }
  addPurchaseItem() {
    this.purchaseOrderItemsFormArray.push(
      this.createDetails
    );
  }

  updateTotalQuantityOnChange(index, value) {
    this.purchaseOrderItemsFormArray[index].totalQuantity = value;
  }

  removePurchaseItem(index) {
    this.alertifyService.confirm('Remove selected Details?', () => {
    this.purchaseOrderItemsFormArray.removeAt(index);
    });

  }

  saveAndPrintPo() {
    const newPoValue = this.poForm.value;
   this.selectedSupplier$.subscribe(x => newPoValue.supplierId = x.id);
    const newPORequest: NewRequest = { data: newPoValue};
    this.poservice.create(newPORequest).subscribe(result => {
      this.alertifyService.message('Purchase Order Created.');
      window.open(this.poservice.getPdfReportingById(result)).print();
    });

  }

  redirectReporting(id) {
    const url = 'home/po/reporting/' + id;
    this.route.navigateByUrl(url);
  }
  ngOnInit(): void {}
}
