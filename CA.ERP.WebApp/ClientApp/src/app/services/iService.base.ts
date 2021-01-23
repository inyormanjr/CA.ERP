import { Observable } from 'rxjs';
import { PaginationResult } from '../models/data.pagination';
import { NewRequest } from '../models/NewRequest';
import { PaginationParams } from '../models/pagination.params';

export interface IServiceBase<T> {
  get(): Observable<T[]>;

  getById(id: any): Observable<T>;

  getByPagination(params: PaginationParams): Observable<PaginationResult<T>>;

  create(createRequest: NewRequest): Observable<any>;

  update(id: any, updateRequest: any): Observable<any>;

  delete(id);
}
