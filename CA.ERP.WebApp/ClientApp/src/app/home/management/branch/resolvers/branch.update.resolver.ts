
import { Injectable } from '@angular/core';
  import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
  import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { BranchService } from '../branch.service';
import { BranchView } from '../model/branch.view';

  @Injectable()
  export class BranchUpdateResolver implements Resolve<BranchView> {

    constructor(private branchService: BranchService,
      private alertify: AlertifyService,
      private router: Router) { }
    resolve(route: ActivatedRouteSnapshot): Observable<BranchView> {
      return this.branchService.getById(route.params.id).pipe(
        catchError((error: any) => {
          this.router.navigate(['../list']);
          this.alertify.error('Resolver Error');
          return of(null);
        })
      );
    }
  }

