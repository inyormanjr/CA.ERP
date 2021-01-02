import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PoEntryComponent } from './po-entry/po-entry.component';
import { PoListComponent } from './po-list/po-list.component';
import { PurchaseOrderPdfViewerComponent } from './purchase-order-pdf-viewer/purchase-order-pdf-viewer.component';
import { PurchaseOrderViewComponent } from './purchase-order-view/purchase-order-view.component';
import { PoListResolverService } from './resolvers/po.list.resolver.service';


const routes: Routes = [{
  path: '', component: PurchaseOrderViewComponent, children: [
    { path: 'list', component: PoListComponent, resolve: {data: PoListResolverService}},
    { path: 'entry', component: PoEntryComponent },
    { path: 'reporting/:id', component: PurchaseOrderPdfViewerComponent},
     {path: '', redirectTo: 'list', pathMatch: 'full'}
  ]}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PurchaseOrderRoutingModule { }
