import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginViewComponent } from './login-view/login-view.component';
import { HomeViewComponent } from './home-view/home-view.component';
import { JwtModule } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { AlertifyService } from './services/alertify/alertify.service';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { EffectsModule } from '@ngrx/effects';
import * as fromMainApp from './reducers/main-app-reducer';
import { AuthModule } from './auth/auth.module';


export function tokenGetter() {
  return localStorage.getItem('token');
}
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FetchDataComponent,
    LoginViewComponent,
    HomeViewComponent,
  ],
  imports: [
    ReactiveFormsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),

    StoreModule.forRoot(
      { 'main-app': fromMainApp.mainAppReducer },
       {runtimeChecks: {strictStateSerializability: true}}
    ),
    AuthModule,
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: environment.production,
    }),
    EffectsModule.forRoot([]),
    FormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter,
        whitelistedDomains: [environment.apiURL],
        blacklistedRoutes: [environment.apiURL + '/api/Authentication'],
      },
    }),
    RouterModule.forRoot([
      { path: 'login', component: LoginViewComponent },
      {
        path: 'home',
        component: HomeViewComponent,
        loadChildren: () =>
          import('../app/home/home.module').then((x) => x.HomeModule),
      },
      { path: 'fetch-data', component: FetchDataComponent },
    ]),
  ],
  providers: [AlertifyService],
  bootstrap: [AppComponent],
})
export class AppModule {}
