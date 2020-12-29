import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { MainAppState } from '../reducers/main-app-reducer';
import { ERP_Main_Actions } from '../reducers/main.action.types';

@Injectable({
  providedIn: 'root'
})
export class HttpIntercepterService implements HttpInterceptor {

  constructor(private store: Store<MainAppState>) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const clone = req.clone();
    this.store.dispatch(ERP_Main_Actions.updateLoadingValue({value: 25}));
    return next
      .handle(req)
      .pipe(
        finalize(() =>
          this.store.dispatch(ERP_Main_Actions.loadMainAppsSuccess())
        )
      );
  }
}
export const HttpRequestInterceptor = {
  provide: HTTP_INTERCEPTORS,
  useClass: HttpIntercepterService,
  multi: true,
};
