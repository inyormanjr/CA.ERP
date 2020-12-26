import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UsersManagementViewComponent } from './users-management-view/users-management-view.component';


const routes: Routes = [{ path: '', component: UsersManagementViewComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
