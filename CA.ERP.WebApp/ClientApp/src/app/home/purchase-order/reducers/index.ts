import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createReducer,
  createSelector,
  MetaReducer,
  on
} from '@ngrx/store';
import { environment } from 'src/environments/environment';
import { PoActionTypes } from '../actions/po.actions.selector';
import { PurchaseOrder } from '../models/new-purchase-order';
import { BrandWithMasterProducts } from '../supplier/models/brandWithMasterProducts';
import { SupplierView } from '../supplier/models/supplier-view';

export const FeatureKey = 'PurchaseOrder';

export interface PurchaseOrderState {
  isLoading: boolean;
  selectedSupplier: SupplierView;
  purchaseOrderList: PurchaseOrder[];
  brandsWithModels: BrandWithMasterProducts[];

}

export const purchaseOrderInitialState: PurchaseOrderState = {
  isLoading: false,
  selectedSupplier: undefined,
  purchaseOrderList: undefined,
  brandsWithModels: undefined
}

export const reducers = createReducer(
  purchaseOrderInitialState,
  on(PoActionTypes.loadPurchaseOrders, (state, action) => {
    return {
      ...state,
      isLoading: true
    }
  }),
  on(PoActionTypes.selectSupplierForPurchaseOrder, (state, action) => {
    return {
      ...state,
      selectedSupplier: action.selectedSupplier
    };
  }),
  on(PoActionTypes.clearSelectedSupplierForPurchaseOrder, (state, action) => {
    return {
      ...state,
      selectedSupplier: undefined
    };
  }),
  on(PoActionTypes.populatePurchaseOrderListView, (state, action) => {
    return {
      ...state,
      isLoading: false,
      purchaseOrderList: action.data
    };
  }),
  on(PoActionTypes.populateBrandsWithModel, (state, action) => {
    return {
      ...state,
      brandsWithModels: action.brandsWithModels,
      isLoading: false
    };
  })
);


export const metaReducers: MetaReducer<PurchaseOrderState>[] = !environment.production
  ? []
  : [];
