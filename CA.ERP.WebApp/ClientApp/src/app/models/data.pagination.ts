
export interface PaginationResult<T> {
  currentPage: number;
  totalPage: number;
  pageSize: number;
  totalCount: number;
  data: T;
}
