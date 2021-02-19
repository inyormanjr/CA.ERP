import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PurchaseOrderService } from 'src/app/home/purchase-order/purchase-order.service';
import { StocksService } from '../../services/stocks.service';

@Component({
  selector: 'app-item-entry',
  templateUrl: './item-entry.component.html',
  styleUrls: ['./item-entry.component.css'],
})
export class ItemEntryComponent implements OnInit {
  stockEntryForm: FormGroup;
  constructor(
    private fB: FormBuilder,
    private poService: PurchaseOrderService,
    private stocksService: StocksService
  ) {
    this.initializeForm();
  }

  initializeForm() {
    this.stockEntryForm = this.fB.group({
      poNumber: ['', Validators.required],
      purchaseOrderId: ['', Validators.required],
      dateReceived: [],
      branchId: ['', Validators.required],
      branchName: [''],
      StockSource: ['', Validators.required],
      supplierId: ['', Validators.required],
      supplierName: [],
      stocks: this.fB.array([], Validators.required),
      deliveryReference: ['', Validators.required],
    });
  }

  trackByFn(index: any, item: any) {
    return index;
  }

  generateStock() {
    this.stocksService
      .generateStocksByPoNumber(this.stockEntryForm.controls.poNumber.value)
      .subscribe((x: any) => {
        x.forEach((data) => {
          (this.stockEntryForm.controls.stocks as FormArray).push(
            this.stock(data)
          );
        });
      });
  }

  stock(data): FormGroup {
    return this.fB.group({
      masterProductId: [data.masterProductId],
      purchaseOrderItemId: [data.purchaseOrderItemId],
      stockNumber: [data.stockNumber],
      serialNumber: [data.serialNumber],
      brandName: [data.brandName],
      model: [data.model],
      stockStatus: [data.stockStatus],
      costPrice: [data.costPrice],
    });
  }

  ngOnInit(): void {}
}
