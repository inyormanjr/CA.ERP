import { Observable } from 'rxjs';
import { NewRequest } from '../models/NewRequest';

export interface ServiceBase<T>
{
  get(): Observable<T[]>;

  getById(id: any): Observable<T>;

  create(createRequest: NewRequest): Observable<any>;

  update(id: any, updateRequest: any): Observable<any>;

  delete(id);
}
