import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DeliveryReceiptViewComponent } from './delivery-receipt-view/delivery-receipt-view.component';


const routes: Routes = [{path: '', component: DeliveryReceiptViewComponent}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DeliveryReceiptRoutingModule { }
