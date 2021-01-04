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

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private store: Store<MainAppState>){}
  intercept(
    req: import('@angular/common/http').HttpRequest<any>,
    next: import('@angular/common/http').HttpHandler
  ): import('rxjs').Observable<import('@angular/common/http').HttpEvent<any>> {
    this.store.dispatch(ERP_Main_Actions.updateLoadingValue({ value: 25 }));

    return next.handle(req).pipe(
      catchError((error) => {
        if (error.status === 401) {
          return throwError(error.error.title);
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
