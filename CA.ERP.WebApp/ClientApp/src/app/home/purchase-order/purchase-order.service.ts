import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PurchaseOrderService {
  baseUrl = environment.apiURL + 'api/purchaseOrder/';
  constructor(private http: HttpClient) { }

  get(): Observable<any[]> {
    return null;
  }

  getById(id: any): Observable<any> {
    return null;
  }

  create(newPurchaseOrder: any) {
    return null;
  }

  update(id: any, updatedPurchaseOrder: any) {}
}
