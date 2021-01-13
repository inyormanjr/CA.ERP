import { MasterProduct } from './masterProduct';

export interface BrandWithMasterProducts {
  supplierId: string;
  brandId: string;
  brandName: string;
  masterProducts: MasterProduct[];
}


