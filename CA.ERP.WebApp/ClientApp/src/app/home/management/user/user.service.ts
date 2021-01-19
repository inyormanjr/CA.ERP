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
    return this.http.get<UserView>(this.baseUrl+id).pipe(
      map(res =>{
        return res;
      },
      error =>{
        console.log(error);
      })
    )
  }
  create(createRequest: any): Observable<any> {
    return this.http.post<any>(this.baseUrl,createRequest).pipe(
      map((res : any) =>{
        return res.id;
      })
    );
  }
  update(id: any, updateRequest: any): Observable<any> {
    return this.http.put<any>(this.baseUrl+id,updateRequest).pipe(
      map((res)=>{
        return res;
      })
    );
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
