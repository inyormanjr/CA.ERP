import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { noop } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { BranchService } from '../branch.service';
import { BranchManagementState } from '../reducers';
import { BranchManagementActions } from '../reducers/branch.actions';



@Injectable()
export class Effects {
  loadBranchs$ = createEffect(() =>
    this.actions$.pipe(
      ofType(BranchManagementActions.fetchBranches),
      tap((action) => {
        this.store.dispatch(BranchManagementActions.fetchingBranches());
        this.branchService.get().pipe(map((x) => {
          this.store.dispatch(BranchManagementActions.loadBranchViewList ({ branchViewList: x }));
        }, (error) => {
          console.log(error);
        })).subscribe(noop, error => {
          this.store.dispatch(BranchManagementActions.loadBranchManagementsFailure({ error }));
          console.log(error);
        });
      })
    ), {dispatch: false}
  );

  constructor(
    private actions$: Actions,
    private branchService: BranchService,
    private store: Store<BranchManagementState>
  ) {}
}
