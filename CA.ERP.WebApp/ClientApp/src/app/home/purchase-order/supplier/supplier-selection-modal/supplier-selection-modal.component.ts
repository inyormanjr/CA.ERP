import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { PoActionTypes } from '../../actions/po.actions.selector';
import { PurchaseOrderState } from '../../reducers';
import { SupplierView } from '../models/supplier-view';
import { SupplierService } from '../services/supplier.service';

@Component({
  selector: 'app-supplier-selection-modal',
  templateUrl: './supplier-selection-modal.component.html',
  styleUrls: ['./supplier-selection-modal.component.css'],
})
export class SupplierSelectionModalComponent implements OnInit {
  supplierList$: Observable<SupplierView[]>;
  filter = { name: '' };
  constructor(
    public bsModalRef: BsModalRef,
    private supplierService: SupplierService,
    private poStore: Store<PurchaseOrderState>
  ) {}

  selectSupplier(supplierView: SupplierView) {
    this.poStore.dispatch(PoActionTypes.selectSupplierForPurchaseOrder({ selectedSupplier: supplierView }));
    this.bsModalRef.hide();
  }

  ngOnInit(): void {
    this.supplierList$ = this.supplierService.get();
  }
}
