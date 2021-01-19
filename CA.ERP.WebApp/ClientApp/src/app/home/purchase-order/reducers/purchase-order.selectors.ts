import { createFeatureSelector, createSelector } from '@ngrx/store';
import { FeatureKey, PurchaseOrderState } from '.';



export const selectPurchaseOrderState = createFeatureSelector<PurchaseOrderState>(FeatureKey);

export const purchaseOrderIsLoding = createSelector(selectPurchaseOrderState, app => app.isLoading);

export const purchaseOrderPaginationResult = createSelector(selectPurchaseOrderState, app => app.purchaseOrdersPaginationResult);

export const selectedSupplier = createSelector(selectPurchaseOrderState, app => app.selectedSupplier);

export const selectBrandsWithModels = createSelector(selectPurchaseOrderState, app => app.brandsWithModels);
