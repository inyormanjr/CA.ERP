import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ManagementViewComponent } from './management-view/management-view.component';


const routes: Routes = [{path: '', component: ManagementViewComponent}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManagementRoutingModule { }
