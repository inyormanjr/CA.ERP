import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { AuthService } from 'src/app/services/auth.service';
import { ServiceBaseService } from 'src/app/services/service-base.service';
import { environment } from 'src/environments/environment';
import { StockView } from '../model/stockview';

@Injectable({
  providedIn: 'root',
})
export class StocksService extends ServiceBaseService<StockView> {
  constructor(httpClient: HttpClient, authService: AuthService) {
    super(environment.apiURL + 'api/Stock/', httpClient, authService);
  }
}
