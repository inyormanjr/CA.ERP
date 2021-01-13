import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { UserManagementState } from '../reducers';
import { UserService } from '../user.service';
import * as userManagementAction from '../action/user-management.actions';
import { Store } from '@ngrx/store';
import { map, tap } from 'rxjs/operators';
import { noop } from 'rxjs';



@Injectable()
export class UserEffects {
  loadUsers$ = createEffect(
    () =>
    this.actions$.pipe(
      ofType(userManagementAction.fetchUsers),
      tap(
        action => {
        this.userManagementStore.dispatch(userManagementAction.fetchingUsers());
        this.userService.get().pipe(
          map(
          x => {
          this.userManagementStore.dispatch(userManagementAction.loadUserViewList ({ usersViewList: x }));
        }, error => {
          console.log(error);
        })).subscribe(noop, error => {
          this.userManagementStore.dispatch(userManagementAction.loadUserManagementsFailure({ error }));
          console.log(error);
        });
      })
    ), {dispatch: false}
  );
  constructor(private actions$: Actions,
    private userService: UserService,
    private userManagementStore: Store<UserManagementState>) { }

}
