import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginationResult } from 'src/app/models/data.pagination';
import { NewRequest } from 'src/app/models/NewRequest';

import { AuthService } from 'src/app/services/auth.service';
import { ServiceBaseService } from 'src/app/services/service-base.service';
import { environment } from 'src/environments/environment';
import { StockEntryRquest } from '../model/stock.request';
import { StockView } from '../model/stockview';

@Injectable({
  providedIn: 'root',
})
export class StocksService extends ServiceBaseService<StockView> {
  constructor(httpClient: HttpClient, authService: AuthService) {
    super(environment.apiURL + 'api/Stock/', httpClient, authService);
  }

  generateStockNumbersByBranchId(
    branchId,
    count
  ): Observable<PaginationResult<string[]>> {
    return this.http.get<PaginationResult<string[]>>(this.baseUrl + '/' + branchId + '/' + count).pipe(map((res: any) => res));
  }

  stockReceive(newRequest: NewRequest) {
    return this.http.post(environment.apiURL + 'api/StockReceive', newRequest);
  }

  generateStocksByPoNumber(poNumber): Observable<any> {
     return this.http.get(
       environment.apiURL +
          'api/StockReceive/GenerateStocks/' + poNumber
     ).pipe((map((res: any) => res.data)));
  }
}
