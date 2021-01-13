import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Routes,RouterModule } from '@angular/router';

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
import { HomeNavComponent } from './home-view/home-nav/home-nav.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ErrorInterceptorProvider } from './error.interceptor';
import { HttpRequestInterceptor } from './intercepter/http-intercepter.service';
import { PdfViewerModule } from 'ng2-pdf-viewer';

import { AuthGuardService as AuthGuard } from './services/auth-guard.service';

export function tokenGetter() {
  return localStorage.getItem('token');
}

const routes : Routes = [
  { path: 'login', component: LoginViewComponent },
  {
    path: 'home',
    component: HomeViewComponent,
    canActivate : [AuthGuard],
    loadChildren: () =>
      import('../app/home/home.module').then((x) => x.HomeModule),
  },
  { path: 'fetch-data', component: FetchDataComponent },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  }
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FetchDataComponent,
    LoginViewComponent,
    HomeViewComponent,
    HomeNavComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    ReactiveFormsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: ['localhost:5001'],
        disallowedRoutes: ['localhost:5001/api/Authentication'],
      },
    }),
    StoreModule.forRoot(
      { mainApp: fromMainApp.mainAppReducer },
      { runtimeChecks: { strictStateSerializability: true } }
    ),
    AuthModule,
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: environment.production,
    }),
    EffectsModule.forRoot([]),
    FormsModule,

    RouterModule.forRoot(routes),
  ],
  providers: [AlertifyService, HttpRequestInterceptor, ErrorInterceptorProvider],
  bootstrap: [AppComponent],
})
export class AppModule { }
