import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { noop } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { ItemManagementActionTypes } from '../actions/item.manage.action.types';
import { StockManageState } from '../reducer/item-management-reducer.reducer';
import { StocksService } from '../services/stocks.service';



@Injectable()
export class ItemManageEffectEffects {
  loadStocksPaginationResul$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(ItemManagementActionTypes.fetchStocksFromApi),
        tap((action) => {
          this.service
            .getByPagination(action.params)
            .pipe(
              map((x: any) => {
                this.store.dispatch(
                  ItemManagementActionTypes.populateStockPaginationResulState({
                    paginationResult: x,
                  })
                );
              })
            )
            .subscribe(noop, (error) => {
              console.log(error);
            });
        })
      ),
    { dispatch: false }
  );

  constructor(private actions$: Actions, private service: StocksService, private store: Store<StockManageState>) {}
}
