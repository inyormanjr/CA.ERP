import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginationResult } from '../models/data.pagination';
import { NewRequest } from '../models/NewRequest';
import { PaginationParams } from '../models/pagination.params';
import { AuthService } from './auth.service';
import { IServiceBase } from './iService.base';

@Injectable({
  providedIn: 'root',
})
export class ServiceBaseService<T> implements IServiceBase<T> {
  constructor(
    public baseUrl: string,
    public http: HttpClient,
    public authService: AuthService
  ) {}
  getByPagination(
    paginationParams?: PaginationParams
  ): Observable<PaginationResult<T>> {
    let params = new HttpParams();

    if (paginationParams.page != null && paginationParams.pageSize != null) {
      params = params.append('page', paginationParams.page.toString());
      params = params.append('pageSize', paginationParams.pageSize.toString());
    }

    if (paginationParams.queryParams != null) {
      paginationParams.queryParams.forEach((x) => {
        params = params.append(x.name, x.value);
      });
    }

    return this.http
      .get<PaginationResult<T[]>>(this.baseUrl, {params}).pipe( map((result: any) =>  result));
  }
  public get(): Observable<T[]> {
    return this.http
      .get<T[]>(this.baseUrl)
      .pipe(map((result: any) => result.data));
  }
  public getById(id: any): Observable<T> {
    return this.http
      .get<T>(this.baseUrl + id)
      .pipe(map((result: any) => result.id));
  }
  public create(createRequest: NewRequest): Observable<any> {
    return this.http.post(this.baseUrl, createRequest);
  }
  public update(id: any, updateRequest: any): Observable<any> {
    return this.http.put(this.baseUrl + id, updateRequest);
  }
  public delete(id: any) {
    this.http.delete(this.baseUrl + id);
  }
}
