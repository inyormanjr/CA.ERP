import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { NewRequest } from 'src/app/models/NewRequest';
import { AuthService } from 'src/app/services/auth.service';
import { ServiceBaseService } from 'src/app/services/service-base.service';
import { environment } from 'src/environments/environment';
import { BranchView } from './model/branch.view';
import { NewBranchRequest } from './model/new.branch';
import { UpdateBranchRequest } from './model/update.branch';

@Injectable({
  providedIn: 'root',
})
export class BranchService extends ServiceBaseService<BranchView> {
  baseUrl = environment.apiURL + 'api/Branch/';
  constructor(http: HttpClient,authService: AuthService) {
    super(environment.apiURL + 'api/Branch/',http,authService)
  }

}
