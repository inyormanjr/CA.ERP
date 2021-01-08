import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ServiceBase } from 'src/app/services/services.base';
import { environment } from 'src/environments/environment';
import { Brand } from '../models/brand';
import { BrandWithMasterProducts } from '../models/brandWithMasterProducts';

@Injectable({
  providedIn: 'root',
})
export class BrandService implements ServiceBase<Brand> {
  baseUrl = environment.apiURL + 'api/brand/';
  constructor(private http: HttpClient) {}
  get(): Observable<Brand[]> {
    return this.http
      .get<Brand[]>(this.baseUrl)
      .pipe(map((res: any) => res.data));
  }
  getById(id: any): Observable<Brand> {
    return this.http
      .get<BrandWithMasterProducts>(this.baseUrl + id)
      .pipe(map((res: any) => res.data));
  }
  create(createRequest: any): Observable<any> {
    return this.http.post(this.baseUrl, createRequest);
  }
  update(id: any, updateRequest: any): Observable<any> {
    return this.http.put(this.baseUrl, +id, updateRequest);
  }
  delete(id: any) {
    this.http.delete(this.baseUrl + id);
  }
}
