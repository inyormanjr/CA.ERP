import { Observable } from 'rxjs';

export interface ServiceBase<T>
{
  get(): Observable<T[]>;

  create(createRequest: any): Observable<any>;

  update(id: any, updateRequest: any): Observable<any>;

  delete(id);
}