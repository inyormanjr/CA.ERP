import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ItemManagementViewComponent } from './item-management-view/item-management-view.component';


const routes: Routes = [{path: '', component: ItemManagementViewComponent}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ItemManagementRoutingModule { }
