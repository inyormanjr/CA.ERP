import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PurchaseOrderViewComponent } from './purchase-order-view/purchase-order-view.component';


const routes: Routes = [{path: '', component: PurchaseOrderViewComponent}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PurchaseOrderRoutingModule { }
