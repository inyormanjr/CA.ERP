import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginationParams } from 'src/app/models/pagination.params';
import { AuthService } from 'src/app/services/auth.service';
import { IServiceBase } from 'src/app/services/iService.base';
import { ServiceBaseService } from 'src/app/services/service-base.service';
import { environment } from 'src/environments/environment';
import { Brand } from '../models/brand';
import { BrandWithMasterProducts } from '../models/brandWithMasterProducts';

@Injectable({
  providedIn: 'root',
})
export class BrandService extends ServiceBaseService<Brand> {

  constructor(httpClient: HttpClient, authService: AuthService) {
    super(environment.apiURL + 'api/brand/', httpClient, authService);
  }
}
