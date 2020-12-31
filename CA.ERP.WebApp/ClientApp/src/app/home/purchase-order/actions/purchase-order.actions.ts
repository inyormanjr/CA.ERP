import { createAction, props } from '@ngrx/store';

export const loadPurchaseOrders = createAction(
  '[PurchaseOrder] Load PurchaseOrders'
);

export const loadPurchaseOrdersSuccess = createAction(
  '[PurchaseOrder] Load PurchaseOrders Success',
  props<{ data: any }>()
);

export const loadPurchaseOrdersFailure = createAction(
  '[PurchaseOrder] Load PurchaseOrders Failure',
  props<{ error: any }>()
);
