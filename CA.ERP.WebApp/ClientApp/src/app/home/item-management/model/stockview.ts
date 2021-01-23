export interface StockView {
  id: string;
  status: number;
  createdAt: Date;
  createdBy: string;
  updatedAt: Date;
  updatedBy: string;
  masterProductId: string;
  stockNumber: string;
  serialNumber: string;
  stockStatus: number;
  costPrice: number;
  brandName: string;
  model: string;
}
