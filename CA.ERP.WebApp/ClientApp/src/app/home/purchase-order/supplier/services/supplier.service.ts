import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ServiceBase } from 'src/app/services/services.base';
import { environment } from 'src/environments/environment';
import { SupplierView } from '../models/supplier-view';

@Injectable({
  providedIn: 'root'
})
export class SupplierService implements ServiceBase<SupplierView> {
  baseUrl = environment.apiURL + 'api/supplier';
  constructor(private http: HttpClient) { }
  get(): Observable<SupplierView[]> {
    return this.http.get<SupplierView[]>(this.baseUrl).pipe(map((response: any) => {
      console.log(response);
      return response.data;
    }));
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


}
