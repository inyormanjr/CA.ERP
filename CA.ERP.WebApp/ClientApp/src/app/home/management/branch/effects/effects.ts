import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { noop } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { fetchBranches, loadBranchViewList } from '../action/branch-management.actions';
import { BranchService } from '../branch.service';
import { BranchManagementState } from '../reducers';



@Injectable()
export class Effects {
  loadBranchs$ = createEffect(() =>
    this.actions$.pipe(
      ofType(fetchBranches),
      tap((action) => {
        this.branchService.get().pipe(map((x) => {
          this.store.dispatch(loadBranchViewList({ branchViewList: x }));
        }, (error) => {
            console.log(error);
        })).subscribe(noop, error => { console.log(error);});
      })
    ), {dispatch: false}
  );

  constructor(
    private actions$: Actions,
    private branchService: BranchService,
    private store: Store<BranchManagementState>
  ) {}
}
