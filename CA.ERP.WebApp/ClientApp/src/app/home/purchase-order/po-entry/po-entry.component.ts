import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
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
import { PoActionTypes } from '../actions/po.actions.selector';
@Component({
  selector: 'app-po-entry',
  templateUrl: './po-entry.component.html',
  styleUrls: ['./po-entry.component.css'],
})
export class PoEntryComponent implements OnInit, OnDestroy {
  branches$: Observable<BranchView[]>;
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
    private alertifyService: AlertifyService,
    private poservice: PurchaseOrderService
  ) {
    this.initializePOFormGroup();

    this.poForm.get('purchaseOrderItems').valueChanges.subscribe((x) => {
      x.forEach((element) => {
        element.totalQuantity = element.orderedQuantity + element.freeQuantity;
        element.totalCostPrice =
          element.orderedQuantity * (element.costPrice - element.discount);
      });
    });

    this.purchaseOrderStore
      .select(PurchaseOrderSelectorType.selectedSupplier)
      .subscribe((x: SupplierView) => {
        if (x !== undefined) {
          this.poForm.controls.supplierId.patchValue(x.id);
          this.poForm.controls.supplierName.patchValue(x.name);
        }
      });

    this.brands$ = this.purchaseOrderStore.select(
      PurchaseOrderSelectorType.selectBrandsWithModels
    );
    this.branches$ = this.branchService.get();
  }
  ngOnDestroy(): void {
    this.purchaseOrderStore.dispatch(
      PoActionTypes.clearSelectedSupplierForPurchaseOrder()
    );
  }

  initializePOFormGroup() {
    this.poForm = this.fb.group({
      supplierId: [undefined, [Validators.required]],
      supplierName: [undefined],
      branchId: [0, [Validators.required, Validators.minLength(2)]],
      poDate: [undefined, [Validators.required]],
      deliveryDate: [undefined, [Validators.required]],
      purchaseOrderItems: this.fb.array([], Validators.required),
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
      orderedQuantity: [0, [Validators.required, Validators.min(1)]],
      freeQuantity: [0, Validators.required],
      totalQuantity: [0, Validators.required],
      costPrice: [this.selectedModel.costPrice, Validators.required],
      discount: [0, Validators.required],
      totalCostPrice: [0, Validators.required],
      deliveryQuantity: [0, Validators.required],
      isNotEditMode: [true, [Validators.required]],
    });
  }

  selectedPoDetals(index): FormGroup {
    return (this.poForm.controls.purchaseOrderItems as FormArray)[index];
  }

  get purchaseOrderItemsFormArray(): FormArray {
    return this.poForm.controls.purchaseOrderItems as FormArray;
  }

  updateCostPrice() {
      this.poForm.controls.purchaseOrderItems.value[
        0
      ].isNotEditMode = !this.poForm.controls.purchaseOrderItems.value[
        0
      ].isNotEditMode;
    this.poForm.controls.purhcaseOrderitems.markAsUntouched();

  }
  addPurchaseItem() {
    for (
      let index = 0;
      index < this.purchaseOrderItemsFormArray.controls.length;
      index++
    ) {
      const element = this.purchaseOrderItemsFormArray.controls[index];
      if (
        element.value.masterProductId ===
        this.createDetails.controls.masterProductId.value
      ) {
        this.alertifyService.error('Selected model already exist');
        return;
      }
    }
    this.purchaseOrderItemsFormArray.push(this.createDetails);
  }

  removePurchaseItem(index) {
    this.alertifyService.confirm('Remove selected Details?', () => {
      this.purchaseOrderItemsFormArray.removeAt(index);
    });
  }

  resetInputs() {
    this.poForm.reset();
    this.selectedSupplier = null;
    this.selectedBrand = null;
    this.selectedModel = null;
    this.purchaseOrderStore.dispatch(
      PoActionTypes.clearSelectedSupplierForPurchaseOrder()
    );
    this.purchaseOrderItemsFormArray.clear();
  }
  saveAndPrintPo() {
    this.alertifyService.confirm('Save new Purchase Order?', () => {
      const newPoValue = this.poForm.value;
      const newPORequest: NewRequest = { data: newPoValue };
      this.poservice.create(newPORequest).subscribe(
        (result) => {
          this.alertifyService.message(
            'Purchase Order Created. Redirecting to printing.'
          );
          this.resetInputs();
          setTimeout(() => {
            window.open(this.poservice.getPdfReportingById(result)).print();
          }, 3000);
        },
        (error) => {
          this.alertifyService.error(error);
        }
      );
    });
  }
  ngOnInit(): void {}
}
