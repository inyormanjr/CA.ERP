import { PaginationParams } from 'src/app/models/pagination.params';

export interface PoPaginationParams extends PaginationParams
{
  barcode?: string;
  startDate?: string;
  endDate?: string;
}
