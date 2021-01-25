export interface Stock {
  masterProductId: string;
  purchaseOrderItemId: string;
  stockNumber: string;
  serialNumber: string;
  stockStatus: number;
  costPrice: number;
}

export interface StockEntryRquest {
  purchaseOrderId: string;
  branchId: string;
  stockSource: number;
  supplierId: string;
  stocks: Stock[];
  deliveryReference: string;
}
