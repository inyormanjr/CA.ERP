import { createAction, props } from '@ngrx/store';
import { PaginationResult } from 'src/app/models/data.pagination';
import { PaginationParams } from 'src/app/models/pagination.params';
import { StockView } from '../model/stockview';


export const fetchStocksFromApi = createAction('[Item Management (ListView)] Fetch Data form Api endpoint',
  props<{ params?: PaginationParams }>());


export const populateStockPaginationResulState = createAction('[Item Management ] Populate state after successful api fetching.',
  props<{ paginationResult: PaginationResult<StockView[]> }>());


export const loadItemManageActionssFailure = createAction(
  '[ItemManageActions] Load ItemManageActionss Failure',
  props<{ error: any }>()
);
