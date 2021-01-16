
export interface PaginationResult<T> {
  currentPage: number;
  totalPage: number;
  data: T;
}
