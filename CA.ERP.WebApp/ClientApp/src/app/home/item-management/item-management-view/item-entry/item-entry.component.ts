import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PurchaseOrderService } from 'src/app/home/purchase-order/purchase-order.service';
import { StocksService } from '../../services/stocks.service';

@Component({
  selector: 'app-item-entry',
  templateUrl: './item-entry.component.html',
  styleUrls: ['./item-entry.component.css']
})
export class ItemEntryComponent implements OnInit {
  stockEntryForm: FormGroup;
  constructor(private fB: FormBuilder,
    private poService: PurchaseOrderService,
    private stocksService: StocksService) {
    this.initializeForm();
   }

  initializeForm() {
    this.stockEntryForm = this.fB.group({
      purchaseOrderId: ['', Validators.required],
      branchId: ['', Validators.required],
      StockSource: ['', Validators.required],
      supplierId: ['', Validators.required],
      stocks: [this.fB.array([]), Validators.required],
      deliveryReference: ['', Validators.required]
    });
  }

  generateStock() {
    this.poService.getById(this.stockEntryForm.controls.purcharseOrderId.value).subscribe((x: any) => {
      console.log(x);
    });
  }


  ngOnInit(): void {
  }

}
