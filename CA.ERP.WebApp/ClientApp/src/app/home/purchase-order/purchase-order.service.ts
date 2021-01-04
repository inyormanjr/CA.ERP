import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ServiceBase } from 'src/app/services/services.base';
import { environment } from 'src/environments/environment';
import { PurchaseOrder } from './models/new-purchase-order';

@Injectable({
  providedIn: 'root'
})
export class PurchaseOrderService implements ServiceBase<PurchaseOrder> {
  baseUrl = environment.apiURL + 'api/PurchaseOrder/';
  constructor(private http: HttpClient) { }
  getById(id: any): Observable<PurchaseOrder> {
    throw new Error('Method not implemented.');
  }
  get(): Observable<PurchaseOrder[]> {
    return this.http.get<PurchaseOrder[]>(this.baseUrl).pipe(map((result: any) =>   result.data ));
  }
  create(createRequest: any): Observable<any> {
    return this.http.post<string>(this.baseUrl, createRequest).pipe((map((result: any) => result.id)));
  }
  update(id: any, updateRequest: any): Observable<any> {
    throw new Error('Method not implemented.');
  }
  delete(id: any) {
    throw new Error('Method not implemented.');
  }

  getPdfReportingById(id) {
    return this.baseUrl + id + '/Print';
  }


}
