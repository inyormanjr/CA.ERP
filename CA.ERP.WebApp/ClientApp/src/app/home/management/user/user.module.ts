import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UsersManagementViewComponent } from './users-management-view/users-management-view.component';

import { StoreModule } from '@ngrx/store';
import * as from from './reducers';
import { EffectsModule } from '@ngrx/effects';
import { UserEffects } from './effects/user.effects';
import { UserService } from './user.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxBootstrapModulesModule } from 'src/app/ngx-bootstrap-modules/ngx-bootstrap-modules.module';
import { UserListComponent } from './users-management-view/UserList/user-list.component';
import { UserEntryComponent } from '../user/users-management-view/UserEntry/user-entry.component';

@NgModule({
  declarations: [UsersManagementViewComponent, UserListComponent, UserEntryComponent],
  imports: [
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    UserRoutingModule,
    NgxBootstrapModulesModule,
    StoreModule.forFeature(from.userManagementFeatureKey,
                        from.reducers,{metaReducers : from.metaReducers}),
    EffectsModule.forFeature([UserEffects])
  ],
  providers : [
    UserService
  ]
})
export class UserModule { }
