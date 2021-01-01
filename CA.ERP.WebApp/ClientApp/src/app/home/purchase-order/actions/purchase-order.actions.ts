import { createAction, props } from '@ngrx/store';
import { PurchaseOrder } from '../models/new-purchase-order';
import { SupplierView } from '../supplier/models/supplier-view';

export const loadPurchaseOrders = createAction(
  '[PurchaseOrder] Load PurchaseOrders'
);

export const selectSupplierForPurchaseOrder = createAction(
  '[New Purchase Order] Select a specific supplier to P.O', props<{ selectedSupplier: SupplierView }>());

export const fetchSelectedSupplierReferences = createAction(
  '[Purchase Order (Effects)] Fetch reference of selected supplier'
);

export const populateSupplierReferences = createAction(
  '[Purchase Order] Populate references of selected supplier',
  props < {data: any}>()
);

export const fetchPurchaseOrders = createAction('[Purchase Order] Purchase Order Object List');

export const populatePurchaseOrderListView = createAction(
  '[Purchase Order] populate purchase order list', props<{ data: PurchaseOrder[] }>()
);

export const createPurchaseOrder = createAction(
  '[New Purchase Order] Create Purchase Order',
  props<{data: any}>()
);

export const NewPurchaseOrderCreated = createAction('[New Purchase Order]  New Purchase Order Created');

export const loadPurchaseOrdersSuccess = createAction(
  '[PurchaseOrder] Load PurchaseOrders Success',
  props<{ data: any }>()
);

export const loadPurchaseOrdersFailure = createAction(
  '[PurchaseOrder] Load PurchaseOrders Failure',
  props<{ error: any }>()
);


