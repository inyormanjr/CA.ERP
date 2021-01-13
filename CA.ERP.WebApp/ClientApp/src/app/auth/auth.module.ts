import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import * as fromAuth from './reducers';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../services/auth.service';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule,
    StoreModule.forFeature(fromAuth.authFeatureKey, fromAuth.reducers, {
      metaReducers: fromAuth.metaReducers,
    }),
  ],
  providers: [AuthService],
})
export class AuthModule {}
