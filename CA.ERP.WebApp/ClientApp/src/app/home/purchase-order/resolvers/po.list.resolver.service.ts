import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PoActionTypes } from '../actions/po.actions.selector';
import { PurchaseOrder } from '../models/new-purchase-order';
import { PurchaseOrderService } from '../purchase-order.service';
import { PurchaseOrderState } from '../reducers';

@Injectable({
  providedIn: 'root'
})
export class PoListResolverService implements Resolve<PurchaseOrder[]> {

  constructor(
    private store: Store<PurchaseOrderState>,
    private poservice: PurchaseOrderService) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<PurchaseOrder[]>  {
    return this.poservice.get().pipe(
      catchError((error) => {
        this.store.dispatch(PoActionTypes.loadPurchaseOrdersFailure({ error }));
        return of(null);
      })
    );
  }
}
