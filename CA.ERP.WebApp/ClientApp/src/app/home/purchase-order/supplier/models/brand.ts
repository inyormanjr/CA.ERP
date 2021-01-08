export interface MasterProduct {
  id: string;
  status: number;
  createdAt: Date;
  createdBy: string;
  updatedAt: Date;
  updatedBy: string;
  model: string;
  description: string;
  productStatus: number;
  brandId: string;
}

export interface Brand {
  id: string;
  status: number;
  createdAt: Date;
  createdBy: string;
  updatedAt: Date;
  updatedBy: string;
  name: string;
  description: string;
  masterProducts: MasterProduct[];
}
