import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginationResult } from 'src/app/models/data.pagination';
import { NewRequest } from 'src/app/models/NewRequest';
import { PaginationParams } from 'src/app/models/pagination.params';
import { ApiResponse } from 'src/app/models/response.data';
import { AuthService } from 'src/app/services/auth.service';
import { IServiceBase } from 'src/app/services/iService.base';
import { ServiceBaseService } from 'src/app/services/service-base.service';
import { environment } from 'src/environments/environment';
import { BrandWithMasterProducts } from '../models/brandWithMasterProducts';
import { SupplierView } from '../models/supplier-view';

@Injectable({
  providedIn: 'root',
})
export class SupplierService extends ServiceBaseService<SupplierView> {

  constructor(httpClient: HttpClient, authService: AuthService) {
    super(environment.apiURL + 'api/supplier/', httpClient, authService);
  }

  getBrandsWithMasterProductsBySupplierId(
    id: any
  ): Observable<BrandWithMasterProducts[]> {
    return this.http
      .get<BrandWithMasterProducts[]>(this.baseUrl + id + '/Brands')
      .pipe(map((res: any) => res.data));
  }

  updateMasterProductCostPriceById(supplierId, updateRequest: NewRequest) {
    this.http
      .put(this.baseUrl + supplierId + '/MasterProduct', updateRequest)
      .pipe(map((response: any) => response));
  }
}
