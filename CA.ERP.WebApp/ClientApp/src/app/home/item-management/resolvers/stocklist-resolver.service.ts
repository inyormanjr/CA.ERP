import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PaginationResult } from 'src/app/models/data.pagination';
import { ItemManagementActionTypes } from '../actions/item.manage.action.types';
import { StockView } from '../model/stockview';
import { StockManageState } from '../reducer/item-management-reducer.reducer';
import { StocksService } from '../services/stocks.service';

@Injectable({
  providedIn: 'root'
})
export class StocklistResolverService implements Resolve<PaginationResult<StockView[]>> {

  constructor(private service: StocksService, private store: Store<StockManageState>) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<PaginationResult<StockView[]>>  {
    return this.service.getByPagination({ page: 1, pageSize: 5 }).pipe(
      catchError((error) => {
        this.store.dispatch(ItemManagementActionTypes.loadItemManageActionssFailure({error}));
        return of(null);
      })
    );
  }
}
