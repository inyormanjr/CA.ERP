import { createFeatureSelector, createSelector } from '@ngrx/store';
import { FeatureKey, PurchaseOrderState } from '.';



export const selectPurchaseOrderState = createFeatureSelector<PurchaseOrderState>(FeatureKey);

export const purchaseOrderIsLoding = createSelector(selectPurchaseOrderState, app => app.isLoading);
