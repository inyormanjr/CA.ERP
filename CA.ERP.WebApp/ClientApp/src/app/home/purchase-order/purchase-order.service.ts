import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginationResult } from 'src/app/models/data.pagination';
import { ApiResponse } from 'src/app/models/response.data';
import { AuthService } from 'src/app/services/auth.service';
import { IServiceBase } from 'src/app/services/iService.base';
import { ServiceBaseService } from 'src/app/services/service-base.service';
import { environment } from 'src/environments/environment';
import { PurchaseOrder } from './models/new-purchase-order';
import { PoPaginationParams } from './models/po.pagination.params';

@Injectable({
  providedIn: 'root'
})
export class PurchaseOrderService extends ServiceBaseService<PurchaseOrder> {

  constructor(http: HttpClient, authService: AuthService) {
    super(environment.apiURL + 'api/PurchaseOrder/', http, authService);
  }
  // getByPagination(poParams?: PoPaginationParams): Observable<PaginationResult<PurchaseOrder[]>> {
  //   let params = new HttpParams();
  //   if (poParams.page != null && poParams.pageSize != null) {
  //     params = params.append('page', poParams.page.toString());
  //     params = params.append('pageSize', poParams.pageSize.toString());
  //   }

  //   if (poParams.barcode != null) {
  //     params = params.append('barcode', poParams.barcode);
  //   }

  //   if (poParams.startDate != null) {
  //     params = params.append('startDate', poParams.startDate.toString());
  //   }

  //   if (poParams.endDate != null) {
  //     params = params.append('endDate', poParams.endDate.toString());
  //   }

  //   return this.http
  //     .get<PaginationResult<PurchaseOrder[]>>(this.baseUrl, { params})
  //     .pipe(map((result: any) => {
  //       return result;
  //     }));
  // }

  getPdfReportingById(id) {
    return this.baseUrl + id + '/Print?access_token=' + this.authService.getToken();
  }


}
