import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { BranchService } from '../../management/branch/branch.service';
import { BranchView } from '../../management/branch/model/branch.view';
import { SupplierService } from '../supplier/services/supplier.service';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { SupplierSelectionModalComponent } from '../supplier/supplier-selection-modal/supplier-selection-modal.component';
import { SupplierView } from '../supplier/models/supplier-view';
import { PurchaseOrderState } from '../reducers';
import { Store } from '@ngrx/store';
import { PurchaseOrderSelectorType } from '../reducers/purchase-order.selector.type';
@Component({
  selector: 'app-po-entry',
  templateUrl: './po-entry.component.html',
  styleUrls: ['./po-entry.component.scss'],
})
export class PoEntryComponent implements OnInit {
  branches$: Observable<BranchView[]>;
  selectedSupplier$: Observable<SupplierView>;
  poForm: FormGroup;
  bsModalRef: BsModalRef;
  modalConfig = {
    backdrop: true,
    ignoreBackdropClick: false,
  };
  constructor(
    private branchService: BranchService,
    private fb: FormBuilder,
    private supplierService: SupplierService,
    private modalSerivce: BsModalService,
    private purchaseOrderStore: Store<PurchaseOrderState>
  ) {
    this.selectedSupplier$ = this.purchaseOrderStore.select(PurchaseOrderSelectorType.selectedSupplier);
    this.branches$ = this.branchService.get();
    this.poForm = this.fb.group({
      supplierId: ['', [Validators.required]],
      supplierName: [],
      branchId: ['', [Validators.required]],
      branchName: [],
      poDate: [Date.now, [Validators.required]],
      deliveryDate: [Date.now, [Validators.required]],
      purchaseOrderItems: [],
    });
  }

  selectSupplierModal() {
    this.bsModalRef = this.modalSerivce.show(
      SupplierSelectionModalComponent,
      this.modalConfig
    );
  }

  get createDetails(): FormGroup {
    return this.fb.group({
      masterProductId: ['', Validators.required],
      masterProductName: ['', Validators.required],
      orderedQuantity: [0],
      freeQuantity: [0],
      totalQuantity: [0],
      costPrice: [0],
      discount: [0],
      totalCostPrice: [0],
      deliveryQuantity: [0],
    });
  }

  addPurchaseItem() {
    console.log('test');
  }

  ngOnInit(): void {}
}
