import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./dashboard/dashboard.module').then((x) => x.DashboardModule),
  },
  {
    path: 'po',
    loadChildren: () =>
      import('./purchase-order/purchase-order.module').then(
        (x) => x.PurchaseOrderModule
      ),
  },
  {
    path: 'item-management',
    loadChildren: () =>
      import('./item-management/item-management.module').then(
        (x) => x.ItemManagementModule
      ),
  },
  {
    path: 'delivery-receipt',
    loadChildren: () =>
      import('./delivery-receipt/delivery-receipt.module').then(
        (x) => x.DeliveryReceiptModule
      ),
  },
  { path: 'reports', loadChildren: () => import('./reports/reports.module').then(x => x.ReportsModule) },
  {path: 'management', loadChildren: () => import('./management/management.module').then(x => x.ManagementModule)},
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
