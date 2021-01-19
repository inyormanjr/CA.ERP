import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginationResult } from 'src/app/models/data.pagination';
import { ApiResponse } from 'src/app/models/response.data';
import { AuthService } from 'src/app/services/auth.service';
import { ServiceBase } from 'src/app/services/services.base';
import { environment } from 'src/environments/environment';
import { PurchaseOrder } from './models/new-purchase-order';

@Injectable({
  providedIn: 'root'
})
export class PurchaseOrderService implements ServiceBase<PurchaseOrder> {
  baseUrl = environment.apiURL + 'api/PurchaseOrder/';
  constructor(private http: HttpClient, private authService: AuthService) { }
  getById(id: any): Observable<PurchaseOrder> {
    throw new Error('Method not implemented.');
  }
  get(): Observable<PurchaseOrder[]> {
    return this.http.get<PurchaseOrder[]>(this.baseUrl).pipe(map((result: any) =>   result.data ));
  }

  getByPagination(page = 1, itemsPerPage = 5, barcode?): Observable<PaginationResult<PurchaseOrder[]>> {
    let params = new HttpParams();
    if (page != null && itemsPerPage != null) {
      params = params.append('page', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (barcode != null) {
      params = params.append('barcode', barcode);
    }

    return this.http
      .get<PaginationResult<PurchaseOrder[]>>(this.baseUrl)
      .pipe(map((result: PaginationResult<PurchaseOrder[]>) => result));
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
    return this.baseUrl + id + '/Print?access_token=' + this.authService.getToken();
  }


}
