import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UsersManagementViewComponent } from './users-management-view/users-management-view.component';


@NgModule({
  declarations: [UsersManagementViewComponent],
  imports: [
    CommonModule,
    UserRoutingModule
  ]
})
export class UserModule { }
