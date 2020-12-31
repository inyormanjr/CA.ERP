import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createReducer,
  createSelector,
  MetaReducer
} from '@ngrx/store';
import { environment } from 'src/environments/environment';

export const FeatureKey = 'PurchaseOrder';

export interface PurchaseOrderState {
  isLoading: boolean;

}

export const purchaseOrderInitialState: PurchaseOrderState = {
  isLoading: false
}

export const reducers = createReducer(
    purchaseOrderInitialState
);


export const metaReducers: MetaReducer<PurchaseOrderState>[] = !environment.production
  ? []
  : [];
