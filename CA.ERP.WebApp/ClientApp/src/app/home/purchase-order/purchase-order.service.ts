import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginationResult } from 'src/app/models/data.pagination';
import { ApiResponse } from 'src/app/models/response.data';
import { AuthService } from 'src/app/services/auth.service';
import { ServiceBaseService } from 'src/app/services/service-base.service';
import { environment } from 'src/environments/environment';
import { PurchaseOrder } from './models/new-purchase-order';

@Injectable({
  providedIn: 'root'
})
export class PurchaseOrderService extends ServiceBaseService<PurchaseOrder> {

  constructor(http: HttpClient, authService: AuthService) {
    super(environment.apiURL + 'api/PurchaseOrder/', http, authService);
  }

  getPdfReportingById(id) {
    return (
      environment.apiURL + 'report/PurchaseOrder/' +
      id +
      '/Print?access_token=' +
      this.authService.getToken()
    );
  }


}
