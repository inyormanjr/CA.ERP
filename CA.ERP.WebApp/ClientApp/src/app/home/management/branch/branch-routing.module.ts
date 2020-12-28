import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BranchManagementComponent } from './branch-management/branch-management.component';
import { BranchEntryComponent } from './branch-management/branchEntry/branchEntry.component';
import { BranchListComponent } from './branch-management/BranchList/BranchList.component';


const routes: Routes = [{
  path: '', component: BranchManagementComponent, children: [
    { path: 'list', component: BranchListComponent },
    { path: 'entry', component: BranchEntryComponent },
    { path: '', redirectTo: 'list', pathMatch: 'full'}
  ]
}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BranchRoutingModule { }