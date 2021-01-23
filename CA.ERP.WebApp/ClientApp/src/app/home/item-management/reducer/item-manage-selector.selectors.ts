import { createFeatureSelector, createSelector } from '@ngrx/store';


import * as  FromReducer from './item-management-reducer.reducer';


export const selectItemManagementState = createFeatureSelector<FromReducer.StockManageState>
  (FromReducer.itemManagementReducerFeatureKey);


export const selectItemManageIsLoading = createSelector(selectItemManagementState, app => app.isLoading);

export const selectStockPaginationResult = createSelector(selectItemManagementState, app => app.stocksPaginationResult);
