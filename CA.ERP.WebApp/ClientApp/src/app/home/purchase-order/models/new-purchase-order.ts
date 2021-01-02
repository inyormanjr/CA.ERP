export interface PurchaseOrder {
  id: string;
  deliveryDate: Date;
  supplierId: string;
  supplierName: string;
  branchId: string;
  branchName: string;
  barcode: string;
  purchaseOrderItems: PurchaseOrderItem[];
}

export interface PurchaseOrderItem {
  masterProductId: string;
  masterProductName: string;
  orderedQuantity: number;
  freeQuantity: number;
  totalQuantity: number;
  costPrice: number;
  discount: number;
  totalCostPrice: number;
  deliveryQuantity: number;
}
