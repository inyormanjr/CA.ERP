import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { noop } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { PoActionTypes } from '../actions/po.actions.selector';
import { PurchaseOrderService } from '../purchase-order.service';
import { PurchaseOrderState } from '../reducers';
import { SupplierService } from '../supplier/services/supplier.service';



@Injectable()
export class PurchaseOrderEffects {

  loadPurchaseOrders$ = createEffect(() =>
    this.actions$.pipe(ofType(PoActionTypes.fetchPurchaseOrders),
      tap((action) => {
        this.purchaseOrderService
          .getByPagination(action.params)
          .pipe(
            map((x: any) => {
              console.log(x);
              this.store.dispatch(
                PoActionTypes.loadPurchaseOrderPaginationResult({
                  paginationResult: x,
                })
              );
            })
          )
          .subscribe(noop, (error) => {
            console.log(error);
          });
    })
    ), {dispatch: false}
  );



  fetchBrandsWithMasterProductsBySupplierId$ = createEffect(() =>
    this.actions$.pipe(ofType(PoActionTypes.fetchBrandsWithMasterproductsOfSupplier),
      tap((action) => {
         this.supplierService
           .getBrandsWithMasterProductsBySupplierId(action.supplieView.id)
           .subscribe((data: any) => {
             this.store.dispatch(
               PoActionTypes.populateBrandsWithModel({ brandsWithModels: data })
             );
             this.store.dispatch(
               PoActionTypes.selectSupplierForPurchaseOrder({
                 selectedSupplier: action.supplieView,
               })
             );
           });
    })
    ), {dispatch: false}
  );

  constructor(private actions$: Actions,
    private store: Store<PurchaseOrderState>,
    private purchaseOrderService: PurchaseOrderService,
    private supplierService: SupplierService) { }

}
