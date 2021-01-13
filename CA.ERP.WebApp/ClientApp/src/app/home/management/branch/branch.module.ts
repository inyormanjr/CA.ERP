import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BranchRoutingModule } from './branch-routing.module';
import { BranchManagementComponent } from './branch-management/branch-management.component';
import { StoreModule } from '@ngrx/store';
import * as from from './reducers';
import { EffectsModule } from '@ngrx/effects';
import { Effects } from './effects/effects';
import { BranchService } from './branch.service';
import { BranchListComponent } from './branch-management/BranchList/BranchList.component';
import { BranchEntryComponent } from './branch-management/branchEntry/branchEntry.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxBootstrapModulesModule } from 'src/app/ngx-bootstrap-modules/ngx-bootstrap-modules.module';


@NgModule({
  declarations: [BranchManagementComponent, BranchListComponent, BranchEntryComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    BranchRoutingModule,
    NgxBootstrapModulesModule,
    StoreModule.forFeature(from.FeatureKey, from.reducers, { metaReducers: from.metaReducers }),
    EffectsModule.forFeature([Effects]),
  ],
  providers: [
     BranchService
   ]
})
export class BranchModule { }
