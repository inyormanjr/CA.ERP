import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { noop, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginationResult } from 'src/app/models/data.pagination';
import { ItemManagementActionTypes } from '../../actions/item.manage.action.types';
import { StockView } from '../../model/stockview';
import { ItemManageSelectorType } from '../../reducer/item-manage-selector.types';
import { StockManageState } from '../../reducer/item-management-reducer.reducer';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css'],
})
export class ItemListComponent implements OnInit {
  stockPaginationResult$: Observable<PaginationResult<StockView[]>>;
  searchModel: any = {name: 'StockNo', value: ''};
  constructor(
    private activatedRoute: ActivatedRoute,
    private store: Store<StockManageState>
  ) {
    this.activatedRoute.data
      .pipe(
        map((x: any) => {
          this.store.dispatch(
            ItemManagementActionTypes.populateStockPaginationResulState({
              paginationResult: x.data,
            })
          );
          this.stockPaginationResult$ = this.store.select(
            ItemManageSelectorType.selectStockPaginationResult
          );
        })
      )
      .subscribe(noop);
  }

  pageChange(event) {
    this.store.dispatch(ItemManagementActionTypes.fetchStocksFromApi({
      params: {
        page: event,
        pageSize: 5, queryParams: []
    }}));
  }

  search(page) {


    this.store.dispatch(
      ItemManagementActionTypes.fetchStocksFromApi({
        params: {
          page: page,
          pageSize: 5,
          queryParams: [],
        },
      })
    );
  }

  ngOnInit(): void {}
}
