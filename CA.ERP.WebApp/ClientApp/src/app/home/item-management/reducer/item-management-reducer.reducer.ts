import { Action, createReducer, on } from '@ngrx/store';
import { PaginationResult } from 'src/app/models/data.pagination';
import { ItemManagementActionTypes } from '../actions/item.manage.action.types';
import { StockView } from '../model/stockview';


export const itemManagementReducerFeatureKey = 'itemManagementReducer';

export interface StockManageState {
  isLoading: boolean;
  stocksPaginationResult: PaginationResult<StockView[]>;
}

export const initialState: StockManageState = {
  isLoading: false,
  stocksPaginationResult: undefined,
};


export const reducer = createReducer(
  initialState,
  on(
    ItemManagementActionTypes.populateStockPaginationResulState,
    (state, action) => {
      return {
        ...state,
        stocksPaginationResult: action.paginationResult,
      };
    }
  )
);

