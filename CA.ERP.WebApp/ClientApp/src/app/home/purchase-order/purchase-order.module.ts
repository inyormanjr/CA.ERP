import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PurchaseOrderRoutingModule } from './purchase-order-routing.module';
import { PurchaseOrderViewComponent } from './purchase-order-view/purchase-order-view.component';


@NgModule({
  declarations: [PurchaseOrderViewComponent],
  imports: [
    CommonModule,
    PurchaseOrderRoutingModule
  ]
})
export class PurchaseOrderModule { }
