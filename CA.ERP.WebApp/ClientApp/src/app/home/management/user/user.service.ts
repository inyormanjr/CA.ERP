import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { UserView } from './model/user.view';
import { IServiceBase } from 'src/app/services/iService.base';
import { BranchView } from '../branch/model/branch.view';
import { PaginationParams } from 'src/app/models/pagination.params';
import { ServiceBaseService } from 'src/app/services/service-base.service';
import { AuthService } from 'src/app/services/auth.service';


@Injectable({
  providedIn: 'root',
})
export class UserService extends ServiceBaseService<UserView> {
  baseUrl = environment.apiURL + 'api/User/';
  constructor(httpClient: HttpClient, authService: AuthService) {
    super(environment.apiURL + 'api/User/', httpClient, authService);
  }
  getUserBranches(): Observable<BranchView[]> {
    return this.http
      .get<BranchView[]>(this.baseUrl + 'branch')
      .pipe(map((res: any) => res.data));
  }
}
