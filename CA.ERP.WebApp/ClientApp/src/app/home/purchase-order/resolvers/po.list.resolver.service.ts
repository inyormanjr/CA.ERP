import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PaginationResult } from 'src/app/models/data.pagination';
import { PoActionTypes } from '../actions/po.actions.selector';
import { PurchaseOrder } from '../models/new-purchase-order';
import { PurchaseOrderService } from '../purchase-order.service';
import { PurchaseOrderState } from '../reducers';

@Injectable({
  providedIn: 'root',
})
export class PoListResolverService
  implements Resolve<PaginationResult<PurchaseOrder[]>> {
  constructor(
    private store: Store<PurchaseOrderState>,
    private poservice: PurchaseOrderService
  ) {}
  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<PaginationResult<PurchaseOrder[]>> {
    return this.poservice.getByPagination({page: 1, pageSize: 5, queryParams: []}).pipe(
      catchError((error) => {
        this.store.dispatch(PoActionTypes.loadPurchaseOrdersFailure({ error }));
        return of(null);
      })
    );
  }
}
