import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PoEntryComponent } from './po-entry/po-entry.component';
import { PoListComponent } from './po-list/po-list.component';
import { PurchaseOrderViewComponent } from './purchase-order-view/purchase-order-view.component';


const routes: Routes = [{
  path: '', component: PurchaseOrderViewComponent, children: [
    { path: 'list', component: PoListComponent},
    { path: 'entry', component: PoEntryComponent },
     {path: '', redirectTo: 'list', pathMatch: 'full'}
  ]}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PurchaseOrderRoutingModule { }
