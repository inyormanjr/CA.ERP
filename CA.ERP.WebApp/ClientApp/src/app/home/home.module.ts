import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PurchaseOrderComponent } from './purchase-order/purchase-order.component';
import { ItemManagementComponent } from './item-management/item-management.component';
import { DeliveryReceiptComponent } from './delivery-receipt/delivery-receipt.component';
import { ReportsComponent } from './reports/reports.component';
import { ManagementComponent } from './management/management.component';


@NgModule({
  declarations: [DashboardComponent, PurchaseOrderComponent, ItemManagementComponent, DeliveryReceiptComponent, ReportsComponent, ManagementComponent],
  imports: [
    CommonModule,
    HomeRoutingModule,
  ]
})
export class HomeModule { }
