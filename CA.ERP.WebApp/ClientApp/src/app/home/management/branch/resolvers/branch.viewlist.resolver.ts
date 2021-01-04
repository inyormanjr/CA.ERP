import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BranchService } from '../branch.service';
import { BranchView } from '../model/branch.view';
import { BranchManagementState } from '../reducers';
import { BranchManagementActions } from '../reducers/branch.actions';




@Injectable()
export class BranchViewListResolver implements Resolve<BranchView[]> {

  constructor(
    private store: Store<BranchManagementState>,
    private branchService: BranchService,
    private route: Router) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):  Observable<BranchView[]>  {
    return this.branchService.get().pipe(
      catchError(() => {
        this.store.dispatch(BranchManagementActions.loadBranchManagementsFailure({ error: { status: false } }));
        return of(null);
      })
    );
  }

}
