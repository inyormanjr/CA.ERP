import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UsersManagementViewComponent } from './users-management-view/users-management-view.component';

import { StoreModule } from '@ngrx/store';
import * as from from './reducers';
import { EffectsModule } from '@ngrx/effects';
import { UserEffects } from './effects/user.effects';
import { UserService } from './user.service';


@NgModule({
  declarations: [UsersManagementViewComponent],
  imports: [
    CommonModule,
    UserRoutingModule,
    StoreModule.forFeature(from.userManagementFeatureKey,from.reducers,{metaReducers : from.metaReducers}),
    EffectsModule.forFeature([UserEffects])
  ],
  providers : [
    UserService
  ]
})
export class UserModule { }
