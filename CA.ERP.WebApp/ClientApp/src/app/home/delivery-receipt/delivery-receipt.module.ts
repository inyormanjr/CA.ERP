import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DeliveryReceiptRoutingModule } from './delivery-receipt-routing.module';
import { DeliveryReceiptViewComponent } from './delivery-receipt-view/delivery-receipt-view.component';


@NgModule({
  declarations: [DeliveryReceiptViewComponent],
  imports: [
    CommonModule,
    DeliveryReceiptRoutingModule
  ]
})
export class DeliveryReceiptModule { }
