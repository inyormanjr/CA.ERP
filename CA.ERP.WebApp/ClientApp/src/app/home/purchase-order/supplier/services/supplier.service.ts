import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { NewRequest } from 'src/app/models/NewRequest';
import { ServiceBase } from 'src/app/services/services.base';
import { environment } from 'src/environments/environment';
import { BrandWithMasterProducts } from '../models/brandWithMasterProducts';
import { SupplierView } from '../models/supplier-view';

@Injectable({
  providedIn: 'root',
})
export class SupplierService implements ServiceBase<SupplierView> {
  baseUrl = environment.apiURL + 'api/supplier/';
  constructor(private http: HttpClient) {}

  get(): Observable<SupplierView[]> {
    return this.http.get<SupplierView[]>(this.baseUrl).pipe(
      map((response: any) => {
        return response.data;
      })
    );
  }

  getById(id: any): Observable<SupplierView> {
    return this.http.get<SupplierView>(this.baseUrl + '/' + id).pipe(
      map((response: any) => response.data)
    );
  }

  getBrandsWithMasterProductsBySupplierId(id: any): Observable<BrandWithMasterProducts[]> {
    return this.http.get<BrandWithMasterProducts[]>(
      this.baseUrl + id + '/Brands'
    ).pipe(map((res: any) => res.data));
  }

  create(createRequest: any): Observable<any> {
    return this.http.post(this.baseUrl, createRequest);
  }
  update(id: any, updateRequest: any): Observable<any> {
    return this.http.put(this.baseUrl + id, updateRequest);
  }

  updateMasterProductCostPriceById(supplierId, updateRequest: NewRequest) {
    this.http.put(this.baseUrl + supplierId + '/MasterProduct', updateRequest).pipe(
      map((response: any) => response)
    );
  }
  delete(id: any) {
    this.http.delete(this.baseUrl + id);
  }
}
