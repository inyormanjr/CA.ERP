import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpErrorResponse,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { catchError, finalize } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Store } from '@ngrx/store';
import { MainAppState } from './reducers/main-app-reducer';
import { ERP_Main_Actions } from './reducers/main.action.types';
import { Route, Router } from '@angular/router';
import { AlertifyService } from './services/alertify/alertify.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private store: Store<MainAppState>, private route: Router, private alertify: AlertifyService){}
  intercept(
    req: import('@angular/common/http').HttpRequest<any>,
    next: import('@angular/common/http').HttpHandler
  ): import('rxjs').Observable<import('@angular/common/http').HttpEvent<any>> {
    this.store.dispatch(ERP_Main_Actions.updateLoadingValue({ value: 25 }));

    return next.handle(req).pipe(
      catchError((error) => {
        if (error.status === 401) {
          this.alertify.error('Un-Authorize access');
          this.route.navigateByUrl('login');
          return throwError(error.error);
        }

        if (error instanceof HttpErrorResponse) {
          const applicationError = error.headers.get('Application-Error');
          if (applicationError) {
            console.log(error.headers);
            return throwError(applicationError);
          }
          const serverError = error.error;
          let modalStateErrors = '';
          if (serverError.errors && typeof serverError.errors === 'object') {
            for (const key in serverError.errors) {
              if (serverError.errors[key]) {
                modalStateErrors += serverError.errors[key] + '\n';
              }
            }
          }
          return throwError(modalStateErrors || serverError || 'Server Error');
        }
      })
    );
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true,
};
