import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { UserLogin } from '../models/UserAgg/user.login';
@Injectable({
providedIn: 'root',
})
export class AuthService {
  baseUrl = environment.apiURL + 'api/Authentication/';
  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) {}


  login(model: UserLogin) {
    return this.http.post(this.baseUrl + 'login', model, {reportProgress: true})
      .pipe(map((response: any) => {
        if (response) {
          localStorage.setItem('token', response.token);
        }
    }));
  }

  decodedToken(): any {
    const token = localStorage.getItem('token');
    const decoded = this.jwtHelper.decodeToken(token);
    return decoded;
  }
}
