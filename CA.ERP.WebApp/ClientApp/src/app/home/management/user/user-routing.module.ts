import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserListComponent } from './users-management-view/UserList/user-list.component';
import { UsersManagementViewComponent } from './users-management-view/users-management-view.component';
import { UserEntryComponent } from '../user/users-management-view/UserEntry/user-entry.component';

const routes: Routes = [
  {
    path: '',
    component: UsersManagementViewComponent,
    children: [
      {path : 'list',component : UserListComponent}  ,
      {path: 'entry', component : UserEntryComponent},
      {path : 'update/:id',component : UserEntryComponent},
      { path: '', redirectTo: 'list', pathMatch: 'full'}
    ]
  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
