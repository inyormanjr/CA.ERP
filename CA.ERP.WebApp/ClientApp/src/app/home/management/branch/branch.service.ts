import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { BranchView } from './model/branch.view';
import { NewBranchRequest } from './model/new.branch';
import { UpdateBranchRequest } from './model/update.branch';

@Injectable({
  providedIn: 'root',
})
export class BranchService {
  baseUrl = environment.apiURL + 'api/Branch/';
  constructor(private http: HttpClient) {}

  get(): Observable<BranchView[]> {
    return this.http.get<BranchView[]>(this.baseUrl).pipe(
      map((response: any) => {
        return response.data;
      })
    );
  }

  getById(id: any): Observable<BranchView> {
    return this.http.get<BranchView>(this.baseUrl + id).pipe(map((response: any) => {
      return response;
    }, (error: any) => console.log(error)));
  }

  create(newBranch: NewBranchRequest): Observable<string> {
    return this.http.post(this.baseUrl, newBranch).pipe(
      map((response: any) => {
        return response.id;
      })
    );
  }

  update(id: string, updateBranchRequest: any) {
    console.log(updateBranchRequest);
    return this.http.put(this.baseUrl + id, updateBranchRequest).pipe(map((response: any) => {
      return true;
    }, (error: any) => console.log(error)));
  }

  delete(id: string) {
    return this.http.delete(this.baseUrl + id).pipe(map((response: any) => {
      return true;
    }, error => console.log(error)));
  }
}
