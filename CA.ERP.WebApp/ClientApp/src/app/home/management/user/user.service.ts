import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { UserView } from './model/user.view';
import { ServiceBase } from 'src/app/services/services.base';


@Injectable({
  providedIn: 'root'
})
export class UserService implements ServiceBase<UserView> {
  baseUrl = environment.apiURL + 'api/User/';
  constructor(private http: HttpClient) { }

  getById(id: any): Observable<UserView> {
    throw new Error('Method not implemented.');
  }
  create(createRequest: any): Observable<any> {
    throw new Error('Method not implemented.');
  }
  update(id: any, updateRequest: any): Observable<any> {
    throw new Error('Method not implemented.');
  }
  delete(id: any) {
    throw new Error('Method not implemented.');
  }

  get() {
    return this.http.get<UserView[]>(this.baseUrl).pipe(
      map((res : any) =>{
          return res.data;
      })
    );
  }
}
