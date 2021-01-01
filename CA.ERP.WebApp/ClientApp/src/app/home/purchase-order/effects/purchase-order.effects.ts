import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { noop } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { PoActionTypes } from '../actions/po.actions.selector';
import { PurchaseOrderService } from '../purchase-order.service';
import { PurchaseOrderState } from '../reducers';



@Injectable()
export class PurchaseOrderEffects {

  loadPurchaseOrders$ = createEffect(() =>
    this.actions$.pipe(ofType(PoActionTypes.fetchPurchaseOrders),
      tap((action) => {
        this.purchaseOrderService.get().pipe(map((x) => {
          this.store.dispatch(PoActionTypes.populatePurchaseOrderListView({ data: x }));
        })).subscribe(noop, error => {
          console.log(error);
        });
    })
    ), {dispatch: false}
  );

  constructor(private actions$: Actions,
    private store: Store<PurchaseOrderState>,
    private purchaseOrderService: PurchaseOrderService) { }

}
