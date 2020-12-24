import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import * as fromAuth from './reducers';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule,
    StoreModule.forFeature(fromAuth.authFeatureKey, fromAuth.reducers, {
      metaReducers: fromAuth.metaReducers,
    }),
  ],
  providers: [JwtHelperService, AuthService],
})
export class AuthModule {}
