import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { BranchView } from './model/branch.view';

@Injectable({
  providedIn: 'root',
})
export class BranchService {
  baseUrl = environment.apiURL + 'api/Branch/';
  constructor(private http: HttpClient) { }

  get(): Observable<BranchView[]> {
    return this.http.get<BranchView[]>(this.baseUrl).pipe(map((response: any) => {
      return response.data;
    }));
  }
}
