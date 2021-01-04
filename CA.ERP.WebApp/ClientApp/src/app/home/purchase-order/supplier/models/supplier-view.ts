import { SupplierBrand } from './supplier-brand';

export interface SupplierView {
  id: string;
  status: number;
  createdAt: string;
  createdBy: string;
  updatedAt: string;
  updatedBy: string;
  name: string;
  address: string;
  contactPerson: string;
  supplierBrands: SupplierBrand[];
}




