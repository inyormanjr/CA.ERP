import { QueryParams } from './query.params';

export interface PaginationParams {
  queryParams?: QueryParams[];
  pageSize: number;
  page: number;
}
