import { createAction, props } from '@ngrx/store';
import { PaginationResult } from 'src/app/models/data.pagination';
import { PaginationParams } from 'src/app/models/pagination.params';
import { PurchaseOrder } from '../models/new-purchase-order';
import { PoPaginationParams } from '../models/po.pagination.params';
import { BrandWithMasterProducts } from '../supplier/models/brandWithMasterProducts';
import { SupplierView } from '../supplier/models/supplier-view';

export const loadPurchaseOrders = createAction(
  '[PurchaseOrder] Load PurchaseOrders'
);

export const selectSupplierForPurchaseOrder = createAction(
  '[New Purchase Order] Select a specific supplier to P.O', props<{ selectedSupplier: SupplierView }>());

export const clearSelectedSupplierForPurchaseOrder = createAction(
  '[New Purchase Order] remove selected supplier');

export const fetchSelectedSupplierReferences = createAction(
  '[Purchase Order (Effects)] Fetch reference of selected supplier'
);

export const populateSupplierReferences = createAction(
  '[Purchase Order] Populate references of selected supplier',
  props < {data: any}>()
);

export const populateBrandsWithModel = createAction(
  '[Purchase Order Entry] Populate array of populateBrandsWithModels',
  props<{ brandsWithModels: BrandWithMasterProducts[] }>());


export const fetchBrandsWithMasterproductsOfSupplier = createAction('[Purchase Order] Populate Brands with corresponding models',
  props<{ supplieView: SupplierView }>());

export const fetchPurchaseOrders = createAction('[Purchase Order] Purchase Order Object List',
  props<{ params: PaginationParams }>());

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

export const loadPurchaseOrderPaginationResult = createAction(
  '[Purchase Order] Load pagination result for purchase Order',
  props<{ paginationResult: PaginationResult<PurchaseOrder[]> }>()
);

export const loadPurchaseOrdersFailure = createAction(
  '[PurchaseOrder] Load PurchaseOrders Failure',
  props<{ error: any }>()
);


