import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { NewRequest } from 'src/app/models/NewRequest';
import { environment } from 'src/environments/environment';
import { UserView } from './model/user.view';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiURL + 'api/User/';
  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get<UserView[]>(this.baseUrl).pipe(
      map((res : any) =>{
          return res.data;
      })
    );
  }

}
