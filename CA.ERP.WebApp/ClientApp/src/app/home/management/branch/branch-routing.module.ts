import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BranchManagementComponent } from './branch-management/branch-management.component';


const routes: Routes = [{path: '', component: BranchManagementComponent}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BranchRoutingModule { }
