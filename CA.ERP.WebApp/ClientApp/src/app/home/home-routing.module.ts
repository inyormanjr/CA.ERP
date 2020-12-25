import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DeliveryReceiptComponent } from './delivery-receipt/delivery-receipt.component';
import { ItemManagementComponent } from './item-management/item-management.component';
import { ManagementComponent } from './management/management.component';
import { PurchaseOrderComponent } from './purchase-order/purchase-order.component';
import { ReportsComponent } from './reports/reports.component';


const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'purchase-orders', component: PurchaseOrderComponent },
  { path: 'item-management', component: ItemManagementComponent },
  { path: 'delivery-receipt', component: DeliveryReceiptComponent },
  { path: 'reports', component: ReportsComponent },
  { path: 'management', component: ManagementComponent },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
