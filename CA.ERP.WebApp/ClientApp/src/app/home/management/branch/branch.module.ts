import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BranchRoutingModule } from './branch-routing.module';
import { BranchManagementComponent } from './branch-management/branch-management.component';


@NgModule({
  declarations: [BranchManagementComponent],
  imports: [
    CommonModule,
    BranchRoutingModule
  ]
})
export class BranchModule { }
