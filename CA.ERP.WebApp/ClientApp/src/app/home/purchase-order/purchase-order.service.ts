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
import { PoPaginationParams } from './models/po.pagination.params';

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

  getByPagination(poParams?: PoPaginationParams): Observable<PaginationResult<PurchaseOrder[]>> {
    let params = new HttpParams();
    if (poParams.page != null && poParams.pageSize != null) {
      params = params.append('page', poParams.page.toString());
      params = params.append('pageSize', poParams.pageSize.toString());
    }

    if (poParams.barcode != null) {
      params = params.append('barcode', poParams.barcode);
    }

    if (poParams.startDate != null) {
      params = params.append('startDate', poParams.startDate.toString());
    }

    if (poParams.endDate != null) {
      params = params.append('endDate', poParams.endDate.toString());
    }

    return this.http
      .get<PaginationResult<PurchaseOrder[]>>(this.baseUrl, { params})
      .pipe(map((result: any) => {
        return result;
      }));
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
